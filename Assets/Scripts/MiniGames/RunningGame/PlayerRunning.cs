using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerRunning : MonoBehaviour
{
    private Animator anim;
    public int state = 1;

    public int runDistance = 0;
    
    public bool spawning = true;
    [SerializeField]
    private GameObject spawner;

    public int runGoal = 40000;

    [SerializeField]
    private HealthBar runBar;


    //ATTACK
    private float timeBtwAttacks = 0;

    [SerializeField]
    private Image life1;
    [SerializeField]
    private Image life2;
    [SerializeField]
    private Image life3;

    [SerializeField]
    private Animator cameraAnim;

    [SerializeField]
    private float startTimeBtwAttack;

    [SerializeField]
    private GameObject introScreen;

    [SerializeField]
    private GameObject WinCard;
    [SerializeField]
    private GameObject LoseCard;

    [SerializeField]
    private Transform attackPos;
    [SerializeField]
    private LayerMask whatIsEnemies;
    [SerializeField]
    private float attackRange;

    [SerializeField]
    private TMP_Text countText;

    public bool hitSomeone = false;

    private bool ended = false;
    private bool won = false;

    public bool attacking = false;

    public bool isRunning = false;

    private bool countDownStarted = false;

    [SerializeField]
    private int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
        runBar.SetMaxHealth(runGoal);
        runBar.SetHealth(0);
        introScreen.SetActive(true);
        isRunning = false;
    }


    private IEnumerator Count(){
        countDownStarted = true;
        countText.text = "";
        introScreen.SetActive(false);
        countText.text = "3";
        yield return new WaitForSeconds(1f);
        countText.text = "2";
        yield return new WaitForSeconds(1f);
        countText.text = "1";
        yield return new WaitForSeconds(1f);
        countText.text = "GO!";
        isRunning = true;
        spawner.GetComponent<RunSpawner>().start();
        yield return new WaitForSeconds(1f);
        countText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning == false && countDownStarted == false){
            if(Input.GetKeyDown(KeyCode.Return)){
                StartCoroutine(Count());
            }
        }
        if(isRunning){
            runBar.SetHealth(runDistance);
            runDistance++;
            if(lives <= 0){
                StartCoroutine(Lose());
            }
            if(runDistance >= runGoal){
                StartCoroutine(Win());
            }

            HandleAttack();
            HandleInput();
            changeState();
        }
        if(ended){
            if(Input.GetKeyDown(KeyCode.Return)){
                if(won){
                    GameManager.beatLevel = true;
                    SceneManager.LoadScene("MainWorld");
                }else{
                    GameManager.beatLevel = false;
                    SceneManager.LoadScene("MainWorld");
                }
            }
        }
    }

    private IEnumerator Win(){
        spawner.SetActive(false);
        isRunning = false;
        //ANIMATIONS
        yield return new WaitForSeconds(4f);
        ended = true;
        won = true;
        WinCard.SetActive(true);
    }

    private void HandleInput(){
        if(Input.GetKeyDown(KeyCode.W)){
            if(state < 3){
                state += 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.S)){
            if(state > 1){
                state -= 1;
            }
        }
    }

    private void changeState(){
        if(state == 1){
            transform.position = new Vector2(transform.position.x, -2.8f);
        }else if(state == 2){
            transform.position = new Vector2(transform.position.x, 0.2f);
        }else if(state == 3){
            transform.position = new Vector2(transform.position.x, 3.15f);
        }
    }

    private IEnumerator Lose(){
        spawner.SetActive(false);
        isRunning = false;
        //ANIMATIONS
        yield return new WaitForSeconds(4f);
        ended = true;
        LoseCard.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            cameraAnim.SetTrigger("Shake");
            lives--;
            if(lives == 2){
                life1.color = Color.black;
            }
            if(lives == 1){
                life2.color = Color.black;
            }
        }
    }




    void HandleAttack(){
        if(timeBtwAttacks <= 0){
            if(Input.GetKeyDown(KeyCode.K)){
                anim.SetTrigger("Hit");
                StartCoroutine(switchAttack());
                timeBtwAttacks = startTimeBtwAttack;
            }
        }else{
            timeBtwAttacks -= Time.deltaTime;
        }
        if(attacking == true && hitSomeone == false){
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            foreach(Collider2D enemy in enemies){
                    if(enemy.tag == "Enemy"){
                        cameraAnim.SetTrigger("Shake");
                        GameObject.Destroy(enemy.gameObject);
                        hitSomeone = true;
                    }
            }
        }
        if(attacking == false){
            hitSomeone = false;
        }
    }
    private IEnumerator switchAttack(){
        yield return new WaitForSeconds(0.1f);
        attacking = true;
        yield return new WaitForSeconds(0.2f);
        attacking = false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange); 
    }

}
