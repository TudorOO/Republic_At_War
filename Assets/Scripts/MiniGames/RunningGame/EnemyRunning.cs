using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunning : MonoBehaviour
{

    private int rand;
    private int rnd;
    private int state;
    public bool check = false;

    [SerializeField]
    private GameObject Attack1;
    [SerializeField]
    private GameObject Attack2;
    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        state = 1;
    }

    private IEnumerator uncheckAfterTime(float time){
        check = true;
        yield return new WaitForSeconds(time);
        check = false;
    }

    private void changeState(){

        rand = Random.Range(0, 700);
        if(rand == 0){
            rand = Random.Range(1, 4);
            if(rand == state){
                if(rand == 1){
                    rand++;
                }else if(rand == 3){
                    rand--;
                }else{
                    rand--;
                }
            }
            state = rand;
            StartCoroutine(uncheckAfterTime(0.3f));
        }

        if(state == 1){
            transform.position = new Vector2(transform.position.x, -2.15f);
        }else if(state == 2){
            transform.position = new Vector2(transform.position.x, 0.85f);
        }else if(state == 3){
            transform.position = new Vector2(transform.position.x, 3.75f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(check == false){
            changeState();
        }
        if(attacking == false){
        //    handleAttack();
        }
    }

    private void handleAttack(){
        rnd = Random.Range(0, 1000);
        if(rnd < 100 ){

        }else if( rnd < 500){
            //place 1 at state
            if(rnd % 3 == 0){
                Instantiate(Attack1, new Vector2(transform.position.x - 0.01f, 3.92f), Quaternion.identity);
            }
            if(rnd % 3 == 1){
                Instantiate(Attack1, new Vector2(transform.position.x - 0.01f, 0.9f), Quaternion.identity);
            }
            if(rnd % 3 == 2){
                Instantiate(Attack1, new Vector2(transform.position.x - 0.01f, -2.1f), Quaternion.identity);
            }
            if(rnd % 5 == 0){
                StartCoroutine(attack2(0.3f));
            }
            StartCoroutine(attackRoutine());
        }else if(rnd < 800){
            //place 2 in state up or down
            if(rnd % 3 == 0){
                Instantiate(Attack1, new Vector2(transform.position.x - 0.01f, 3.92f), Quaternion.identity);
                Instantiate(Attack1, new Vector2(transform.position.x - 0.01f, 0.9f), Quaternion.identity);
            }
            if(rnd % 3 == 1){
                Instantiate(Attack1, new Vector2(transform.position.x - 0.01f, 0.9f), Quaternion.identity);
                if(rnd % 2 == 0){
                    Instantiate(Attack1, new Vector2(transform.position.x - 0.01f, 3.92f), Quaternion.identity);
                }else{
                    Instantiate(Attack1, new Vector2(transform.position.x - 0.01f, -2.1f), Quaternion.identity);
                }
            }
            if(rnd % 3 == 2){
                Instantiate(Attack1, new Vector2(transform.position.x - 0.01f, -2.1f), Quaternion.identity);
                Instantiate(Attack1, new Vector2(transform.position.x - 0.01f, 0.9f), Quaternion.identity);
            }
            if(rnd % 5 == 0){
                StartCoroutine(attack2(0.3f));
            }
            StartCoroutine(attackRoutine());
        }else{
            //place bigHit
            Instantiate(Attack2, new Vector2(transform.position.x - 0.01f, 0.61f), Quaternion.identity);
            if(rnd % 5 == 0){
                StartCoroutine(attack2(0.6f));
            }
            StartCoroutine(attackRoutine());
        }
    }

    private IEnumerator attack2(float timeToWait){
        yield return new WaitForSeconds(timeToWait);
        Instantiate(Attack2, new Vector2(transform.position.x - 0.01f, 0.61f), Quaternion.identity);
    }

    private IEnumerator attackRoutine(){
        attacking = true;
        yield return new WaitForSeconds(1f);
        attacking = false;
    }
}
