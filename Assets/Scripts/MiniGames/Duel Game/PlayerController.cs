using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private float timeBtwAttacks;

    [SerializeField]
    private Animator cameraAnim;

    [SerializeField]
    private GameObject introScreen;

    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject loseScreen;

    [SerializeField]
    private float startTimeBtwAttack;

    [SerializeField]
    private Transform attackPos;
    [SerializeField]
    private LayerMask whatIsEnemies;
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private int damage;

    private bool isDashing = false;
    private bool canDash = true;

    public bool hitSomeone = false;

    private bool switchLock = false;

    public bool attacking = false;

    private bool isShielded = false;

    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float moveSpeed = 0;
    [SerializeField]
    private float jumpSpeed = 0;
    [SerializeField]
    private float dashTime = 1f;
    [SerializeField]
    private float dashSpeed = 5f;
    [SerializeField]
    private float dashCoolDown = 1f;

    [Range(0f, 1f)]
    public float drag = 0.9f;

    [SerializeField]
    private BoxCollider2D groundCheck;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    bool grounded = false;

    private SpriteRenderer sp;
    private float shieldGravity;

    private bool hasWon = false;
    private bool hasEnded = false;

    public int shieldHp = 100;

    private bool isGettingHit = false;

    float xInput;
    private bool shieldCoolDown = false;

    [SerializeField]
    private float attack = 100;
    private float hp = 100;

    [SerializeField]
    private HealthBar playerHp;
    [SerializeField]
    private HealthBar playerAttack;
    [SerializeField]
    private HealthBar enemyHp;
    [SerializeField]
    private HealthBar enemyAttack;

    private Rigidbody2D rb;
    private Animator anim;
    public bool isRunning;
    [SerializeField]
    private TMP_Text countText;
    [SerializeField]
    private GameObject enemyObj;
    [SerializeField]
    private GameObject enemySprite;
    [SerializeField]
    private Animator enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        InitializeHpBars();
        countText.text = "";
        introScreen.SetActive(true);
    }

    void FixedUpdate(){
        if(isRunning){
            CheckGrounded();
            if(grounded && xInput == 0 && rb.velocity.y <= 0){
                rb.velocity *= drag;
            }
            if(!isDashing && !isShielded && !isGettingHit){
                MoveInput();
            }
            HandleAnimation();
            if(!isShielded){
                if(attack < 100){
                    attack += 0.2f;
                }
            }else{
                if(attack > 0){
                    attack -= 0.5f;
                }
            }
        }
    }

    private IEnumerator Count(){
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
        enemySprite.SetActive(false);
        enemyObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        countText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !hasEnded && !isRunning){
            StartCoroutine(Count());
        }

        if(isRunning){
            if(Input.GetKeyDown(KeyCode.P)){
                GetHit(10, 30, false);
            }
            CheckInput();
            if(!isDashing && !isGettingHit){
                if(!isShielded){
                    HandleJump();
                    HandleAttack();
                }
                HandleShield();
            }
            if(Input.GetKeyDown(KeyCode.J) && canDash && attacking == false && !isShielded && !isGettingHit){
                StartCoroutine(Dash());
            }
            playerHp.SetHealth((int)hp);
            if(hp <= 0){
                StartCoroutine(End(false));
            }
            
            playerAttack.SetHealth((int)attack);
        }

        if(hasEnded){
            if(hasWon){
                if(Input.GetKeyDown(KeyCode.Return)){
                    GameManager.beatLevel = true;
                    SceneManager.LoadScene("MainWorld");
                }
            }else{
                if(Input.GetKeyDown(KeyCode.Return)){
                    GameManager.beatLevel = false;
                    SceneManager.LoadScene("MainWorld");
                }
            }
        }
    }

    private IEnumerator End(bool won){
        isRunning = false;
        if(won==false){
            anim.SetTrigger("Death");
            hasEnded = true;
            yield return new WaitForSeconds(3f);
            loseScreen.SetActive(true);
            enemyObj.SetActive(false);
        }else{
            enemyAnim.SetTrigger("Death");
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            enemyAnim.SetBool("isRunning", false);
            hasWon = true;
            hasEnded = true;
            yield return new WaitForSeconds(3f);
            winScreen.SetActive(true);
            enemyObj.SetActive(false);
        }
    }

    public void EnemyDied(){
        if(!hasWon){
            StartCoroutine(End(true));
            hasWon = true;
        }
    }

    void HandleShield(){
        if(attack <= 0){
            shieldCoolDown = true;
            isShielded = false;
            sp.color = Color.white;
            anim.SetBool("isShielded", false);
        }
        if(attack >= 100){
            attack = 100;
            shieldCoolDown = false;
            playerAttack.setBarColor(Color.white);
        }
        if(shieldCoolDown == true){
            playerAttack.setBarColor(Color.green);
        }
        if(Input.GetKeyDown(KeyCode.L) && shieldCoolDown == false){
            isShielded = true;
            shieldGravity = rb.gravityScale;
            rb.gravityScale = 0;
            sp.color = Color.blue;
            anim.SetBool("isShielded", true);
            rb.velocity = new Vector2(0, 0);
        }
        if(Input.GetKeyUp(KeyCode.L)){
            isShielded = false;
            rb.gravityScale = shieldGravity;
            sp.color = Color.white;
            anim.SetBool("isShielded", false);
        }
    }

    void HandleAnimation(){
        if(rb.velocity.y != 0){
            anim.SetBool("isJumping", true);
        }else if(rb.velocity.x != 0){
            anim.SetBool("isRunning", true);
        }
        if(rb.velocity.y == 0){
            anim.SetBool("isJumping", false);
        }
        if(rb.velocity.x == 0){
            anim.SetBool("isRunning", false);
        }
    }

    void MoveInput(){

        if(Mathf.Abs(xInput) > 0){
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(rb.velocity.x + increment, -moveSpeed, moveSpeed);



            rb.velocity = new Vector3(newSpeed, rb.velocity.y);

            float direction = Mathf.Sign(xInput);
            if(switchLock == false){
                transform.localScale = new Vector3(direction*5, 5, 1);
            }
        }
    }

    private IEnumerator Dash(){
        isDashing = true;
        canDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2((transform.localScale.x / 5) * dashSpeed, 0);
        anim.SetTrigger("Dash");
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    void HandleJump(){
        if(Input.GetButtonDown("Jump") && grounded){
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed);
        }
    }

    void CheckInput(){
        xInput = Input.GetAxis("Horizontal");
    }

    void CheckGrounded(){
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask). Length > 0;
    }
    void HandleAttack(){
        if(timeBtwAttacks <= 0){
            if(Input.GetKeyDown(KeyCode.K)){
                playerAnim.SetTrigger("Hit");
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
                        enemy.GetComponent<DuelEnemyController>().GetHit(damage);
                        cameraAnim.SetTrigger("Shake");
                        hitSomeone = true;
                    }
            }
        }
        if(attacking == false){
            hitSomeone = false;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange); 
    }

    // private IEnumerator dealDamageToEnemy(Collider2D enemy){
    //     yield return new WaitForSeconds(0.2f);
    //     enemy.GetComponent<DuelEnemyController>().GetHit(damage);
    //     cameraAnim.SetTrigger("Shake");
    // }

    private IEnumerator switchAttack(){
        switchLock = true;
        yield return new WaitForSeconds(0.1f);
        attacking = true;
        yield return new WaitForSeconds(0.2f);
        attacking = false;
        switchLock = false;
    }

    void InitializeHpBars(){
        playerHp.SetMaxHealth(100);
        enemyAttack.SetMaxHealth(100);
        enemyHp.SetMaxHealth(100);
        playerAttack.SetMaxHealth(100);
    }

    public void GetHit(int damage, int shieldDamage, bool isRight){
        if(isShielded){
            attack -= shieldDamage;
            //Animation
        }else{
            if(!isGettingHit){
                CheckInput();
                hp -= damage;
                anim.SetTrigger("getHit");
                StartCoroutine(HitRoutine(isRight));
            }
        }
    }
    private IEnumerator HitRoutine(bool isRight){
        isGettingHit = true;
        if(isRight == true){
            rb.velocity = new Vector2(7f, 10f);
        }else{
            rb.velocity = new Vector2(-7f, 10f);
        }
        Debug.Log("Changed Velocity");
        yield return new WaitForSeconds(0.8f);
        isGettingHit = false;
        rb.velocity = new Vector2(0, 0);
    }
}
