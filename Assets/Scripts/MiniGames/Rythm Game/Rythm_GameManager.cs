using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Rythm_GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject NoteHolder;
    [SerializeField]
    private int enemyMultiplier;

    [SerializeField]
    private GameObject IntroScreen;
    [SerializeField]
    private GameObject EndingScreen_W;
    [SerializeField]
    private GameObject EndingScreen_L;

    [SerializeField]
    private AudioSource WonAd;
    [SerializeField]
    private AudioSource GameOverAd;
    [SerializeField]
    private AudioSource countAd;
    [SerializeField]
    private AudioSource hitNoteSfx;
    [SerializeField]
    private AudioSource DealDamageSfx;
    [SerializeField]
    private AudioSource NoteMissedSfx;
    [SerializeField]
    private AudioSource virusMusic;


    [SerializeField]
    private AudioSource music;

    [SerializeField]
    private bool startPlaying;

    private int enemyHealth;
    private int playerHealth;
    private int enemyAttack;
    private int playerAttack;

    private int currentMultiplier;
    private int multiplierTracker;

    [SerializeField]
    private TMP_Text timer;

    [SerializeField]
    private int attackPerNote = 30;
    [SerializeField]
    private int attackPerGoodNote = 10;
    [SerializeField]
    private int attackPerPerfectNote = 20;

    private bool isMusicPaused = false;

    [SerializeField]
    private int[] MultiplierThresholds;

    [SerializeField]
    private BeatScroller bs;

    private bool canReturn = false;
    private bool beatLevel = false;

    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private Animator enemyAnim;

    public static Rythm_GameManager instance;

    [SerializeField]
    private HealthBar healthBar_P;
    [SerializeField]
    private HealthBar healthBar_E;

    [SerializeField]
    private Color[] MultiplierColors;

    [SerializeField]
    private float virusThreshHold;

    private bool countDownStarted = false;

    [SerializeField]
    private HealthBar attackBar_P;
    [SerializeField]
    private HealthBar attackBar_E;

    [SerializeField]
    private TMP_Text multiText;

    [SerializeField]
    private float VirusMultifier = 1;
    [SerializeField]
    private float SpawnTime = 0.35f;
    [SerializeField]
    private int playerDefaultDamage;
    [SerializeField]
    private AudioSource virusEffect;

    private IEnumerator virusize(){
        enemyHealth = 100;
        virusThreshHold = -1000;
        foreach(Transform child in NoteHolder.transform){
            Destroy(child.gameObject);
        }
        NoteHolder.SetActive(false);
        music.Stop();
        enemyAnim.SetTrigger("viruseze");
        yield return new WaitForSeconds(6f);
        virusEffect.Play();
        yield return new WaitForSeconds(1f);
        virusMusic.Play();
        enemyHealth = 150;
        playerDefaultDamage = 6;
        healthBar_E.SetHealth(enemyHealth);
        bs.virusMultifier  = VirusMultifier;
        bs.spawnTime = SpawnTime;
        NoteHolder.SetActive(true);
        NoteHolder.GetComponent<BeatScroller>().zeroArChance = 10;
        NoteHolder.GetComponent<BeatScroller>().oneArChance = 80;
        NoteHolder.GetComponent<BeatScroller>().twoArChance = 10;
        NoteHolder.GetComponent<BeatScroller>().beatTempo = 195;
        NoteHolder.GetComponent<BeatScroller>().spawnTime = 0.3f;

        bs.isSpawning = false;
    }


    void Start()
    {
        instance = this;

        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "x1";
        multiText.color = MultiplierColors[0];

        SetupHealthAndAttackBars(); 
    }

    // Update is called once per frame
    void Update()
    {

        if(enemyHealth < virusThreshHold && virusThreshHold != -1000){
            StartCoroutine(virusize());
            Debug.Log("virusat!");
        }

        if(canReturn == true){
            if(Input.GetKeyDown(KeyCode.Return)){
                if(beatLevel == true){
                    GameManager.beatLevel = true;
                    SceneManager.LoadScene("MainWorld");
                }else{
                    GameManager.beatLevel = false;
                    SceneManager.LoadScene("MainWorld");
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.P)){
            PlayerDealsDamage(10);
        }if(Input.GetKeyDown(KeyCode.E)){
            EnemyDealsDamage(10);
        }

        if(PlayerPrefs.GetInt("isInMenu") == 1){
            NoteHolder.gameObject.SetActive(false);
            bs.isSpawning = false;
            music.Pause();
            isMusicPaused = true;
        }else{
            if(isMusicPaused){
                isMusicPaused = false;
                StartCoroutine(Countdown());
            }
        }


        if(!startPlaying){
            if(Input.GetKeyDown(KeyCode.Return) && countDownStarted == false){
                Destroy(IntroScreen.gameObject);
                StartCoroutine(Countdown());
            }
        }


        if(playerAttack >= 500){
            DealDamageSfx.Play();
            PlayerDealsDamage(playerDefaultDamage * currentMultiplier);
            playerAttack = 0;
            attackBar_P.SetHealth(playerAttack);
        }
        if(enemyAttack >= 500){
            DealDamageSfx.Play();
            EnemyDealsDamage(enemyMultiplier * Random.Range(2, 5));
            enemyAttack = 0;
            attackBar_E.SetHealth(enemyAttack);
        }

        if(playerHealth <= 0){
            playerAnim.SetTrigger("Death");
            playerHealth = 10000;
            enemyAttack = -10000;
            StartCoroutine(PlayerDeath());
        }
        if(enemyHealth <= 0 && virusThreshHold == -1000){
            enemyAnim.SetTrigger("Death");
            enemyHealth = 1000;
            enemyAttack = -1000000;
            StartCoroutine(EnemyDeath());
        }

    }

    private IEnumerator Countdown(){
        countDownStarted = true;
        timer.text = "3";
        countAd.Play();
        yield return new WaitForSeconds(1f);
        timer.text = "2";
        countAd.Play();
        yield return new WaitForSeconds(1);
        timer.text = "1";
        countAd.Play();
        yield return new WaitForSeconds(1);
        timer.text = "START!";
        NoteHolder.gameObject.SetActive(true);
        startPlaying = true;
        bs.isRunning = true;
        music.Play();
        yield return new WaitForSeconds(1);
        timer.text = "";
    }

    private IEnumerator PlayerDeath(){
        NoteHolder.gameObject.SetActive(false);
        music.Stop();
        virusMusic.Stop();
        yield return new WaitForSeconds(2f);
        EndingScreen_L.gameObject.SetActive(true);
        GameOverAd.Play();
        beatLevel = false;
        canReturn = true;
    }

    private IEnumerator EnemyDeath(){
        NoteHolder.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        music.Stop();
        virusMusic.Stop();
        WonAd.Play();
        EndingScreen_W.gameObject.SetActive(true);
        beatLevel = true;
        canReturn = true;
    }

    public void NoteHit(bool p){
        if(p == true){
            hitNoteSfx.Play();
            if(currentMultiplier - 1 < MultiplierThresholds.Length){
                multiplierTracker++;
                if(MultiplierThresholds[currentMultiplier-1] <= multiplierTracker){
                    currentMultiplier++;
                    multiplierTracker = 0;
                    multiText.text = "x" + currentMultiplier;
                    multiText.color = MultiplierColors[currentMultiplier-1];
            }
            }
            playerAttack += attackPerNote;
            attackBar_P.SetHealth(playerAttack);
        }else{
            enemyAttack += attackPerNote;
            attackBar_E.SetHealth(enemyAttack);
        }
    }

    public void NoteMissed(){
        NoteMissedSfx.Play();
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "x" + currentMultiplier;
        multiText.color = MultiplierColors[currentMultiplier-1];
    }



public void NormalHit(bool p){

    NoteHit(p);
}
public void GoodHit(bool p){
    if(p == true){
        playerAttack += attackPerGoodNote;
    }else{
        enemyAttack += attackPerGoodNote;
    }
    NoteHit(p);
}
public void PerfectHit(bool p){

    if(p == true){
        playerAttack += attackPerPerfectNote;
    }else{
        enemyAttack += attackPerPerfectNote;
    }
    NoteHit(p);
}


    private void SetupHealthAndAttackBars(){
        healthBar_P.SetMaxHealth(150);
        healthBar_E.SetMaxHealth(150);
        attackBar_P.SetMaxHealth(500);
        attackBar_E.SetMaxHealth(500);
        attackBar_E.SetHealth(0);
        attackBar_P.SetHealth(0);
        enemyHealth = 150;
        playerHealth = 150;
        enemyAttack = 0;
        playerAttack = 0;
    }


    private void PlayerDealsDamage(int damage){
        playerAnim.SetTrigger("Attack");
        playerAnim.SetBool("canIdle", false);
        StartCoroutine(EnemyTakesDamage(damage));
    }
    private void EnemyDealsDamage(int damage){
        enemyAnim.SetTrigger("Attack");
        enemyAnim.SetBool("canIdle", false);
        StartCoroutine(PlayerTakesDamage(damage));
    }

    private IEnumerator EnemyTakesDamage(int damage){
        yield return new WaitForSeconds(0.3f);
        enemyHealth -= damage;
        healthBar_E.SetHealth(enemyHealth);
        enemyAnim.SetTrigger("GetHit");
        yield return new WaitForSeconds(0.1f);
        playerAnim.SetBool("canIdle", true);
    }
    private IEnumerator PlayerTakesDamage(int damage){
        yield return new WaitForSeconds(0.03f);
        playerHealth -= damage;
        healthBar_P.SetHealth(playerHealth);
        playerAnim.SetTrigger("GetHit");
        yield return new WaitForSeconds(0.3f);
        enemyAnim.SetBool("canIdle", true);
    }
}
