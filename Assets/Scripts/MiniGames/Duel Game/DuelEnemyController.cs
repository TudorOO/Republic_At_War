using System.Collections;
using System.Collections.Generic;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class DuelEnemyController : MonoBehaviour
{
    private Transform player;
    private bool hitSomeone = false;
    private Animator anim;

    private bool isGettingHit = false;

    public bool attacker = false;
    [SerializeField]
    private Animator cameraAnim;
    [SerializeField]
    private float startTimeBtwAttack = 1f;
    [SerializeField]
    private Transform attackPos;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private LayerMask whatIsEnemies;

    public bool attack1 = false;
    public bool attack2 = false;

    [SerializeField]
    private GameObject atck1Feed;
    [SerializeField]
    private GameObject atck1;
    [SerializeField]
    private GameObject atck2Feed;
    [SerializeField]
    private GameObject atck2;

    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private BoxCollider2D groundCheck;

    [SerializeField]
    private LayerMask groundMask;

    private Rigidbody2D rb;

    [SerializeField]
    private HealthBar enemyHp;
    [SerializeField]
    private HealthBar enemyAttack;

    public int Hp = 100;
    public bool isAttacking = true;

    private bool switchLock = false;

    private float timeBtwAttacks;
    public bool grounded = false;
    private bool hasEnded;

    public void Jump(bool isRight){
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask). Length > 0;
        if(grounded){
            if(isRight){
                rb.velocity = new Vector3(7f, jumpSpeed);
            }else{
                rb.velocity = new Vector3(-7f, jumpSpeed);
            }
            grounded = false;
        }
    }

    public void GetHit(int damage){
        if(!isGettingHit){
            Hp-= damage;
            anim.SetTrigger("getHit");
            anim.SetBool("isRunning", false);
            Debug.Log("Guy Attacked");
            if(player.position.x - transform.position.x < 0){
                StartCoroutine(HitRoutine(true));
            }else{
                StartCoroutine(HitRoutine(false));
            }
        }
    }


    public IEnumerator Attack1(){
        int right;
        anim.SetBool("isRunning", false);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0f, 0f);
        anim.SetBool("Attack1", true);
        anim.SetTrigger("Attack1Trig");
        atck1Feed.SetActive(true);
        if(player.position.x - transform.position.x < 0){
            right = 1;
        }else{
            right = 2;
        }
        yield return new WaitForSeconds(0.4f);
        atck1Feed.SetActive(false);
        atck1.SetActive(true);
        if(right == 1){
            transform.position = new Vector2(-8f, transform.position.y);
        }else{
            transform.position = new Vector2(8f, transform.position.y);
        }
        yield return new WaitForSeconds(0.03f);
        //SOUND EFFECT
        atck1.SetActive(false);
        yield return new WaitForSeconds(0f);
        rb.gravityScale = originalGravity;
        anim.SetBool("Attack1", false);
        anim.SetBool("isRunning", true);
        anim.SetBool("isJumping", false);
    }

    public IEnumerator Attack2(){
        int right;
        anim.SetBool("isRunning", false);
        float originalGravity = rb.gravityScale;
        rb.velocity = new Vector2(0f, 0f);
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0f, 0f);
        SwitchPlayer();
        anim.SetBool("Attack1", true);
        anim.SetTrigger("Attack1Trig");
        atck2Feed.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        atck2Feed.SetActive(false);
        atck2.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        //SOUND EFFECT
        atck2.SetActive(false);
        yield return new WaitForSeconds(0f);
        rb.gravityScale = originalGravity;
        anim.SetBool("Attack1", false);
        anim.SetBool("isRunning", true);
        anim.SetBool("isJumping", false);
    }
    private IEnumerator HitRoutine(bool isRight){
        isGettingHit = true;
        if(isRight == true){
            rb.velocity = new Vector2(4f, 6f);
        }else{
            rb.velocity = new Vector2(-4f, 6f);
        }
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isRunning", true);
        isGettingHit = false;
        rb.velocity = new Vector2(0, 0);
    }

    private void SwitchPlayer(){
        if(player.position.x - transform.position.x <0){
            transform.localScale = new Vector3(5, 5, 1);
        }else{
            transform.localScale = new Vector3(-5, 5, 1);
        }
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.X)){
            StartCoroutine(Attack1());
        }
        if(!hasEnded){
            if(!switchLock){
                SwitchPlayer();
            }
            enemyHp.SetHealth((int)Hp);
            if(attack1){
                StartCoroutine(Attack1());
                attack1 = false;
            }else if(attack2){
                StartCoroutine(Attack2());
                attack2 = false;
            }else{
                Attack();
            }
            if(Hp <= 0){
                player.GetComponent<PlayerController>().EnemyDied();
                hasEnded = true;
            }
        }else{
            anim.SetBool("isRunning", false);
        }
    }
    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("isRunning", true);
    }

    public void Attack(){
        if(timeBtwAttacks <= 0){
            if(attacker == true){
                StartCoroutine(atckroutine());
                timeBtwAttacks = startTimeBtwAttack;
                attacker = false;
            }
        }else{
            timeBtwAttacks -= Time.deltaTime;
        }
        if(isAttacking == false){
            hitSomeone = false;
        }
    }
    public void attackTrigger(){
        if(isAttacking == true && hitSomeone == false){
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            if(enemies.Length > 0){
                foreach(Collider2D enemy in enemies){
                    if(enemy.tag == "Player"){
                        if(player.position.x - transform.position.x > 0){
                            enemy.GetComponent<PlayerController>().GetHit(10, 20, true);
                        }else{
                            enemy.GetComponent<PlayerController>().GetHit(10, 20, false);
                        }
                    }
                }
                cameraAnim.SetTrigger("Shake");
                hitSomeone = true;
            }
        }
    }

    public IEnumerator atckroutine(){
        anim.SetTrigger("Hit2");
        anim.SetBool("isRunning", false);
        isAttacking = true;
        Debug.Log("isAttacking");
        switchLock = true;
        yield return new WaitForSeconds(0.7f);
        switchLock = false;
        isAttacking = false;
        anim.SetBool("isRunning", true);
        Debug.Log("StoppedAttacking");
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange); 
    }
    public void CheckGrounded(){
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask). Length > 0;
    }
}
