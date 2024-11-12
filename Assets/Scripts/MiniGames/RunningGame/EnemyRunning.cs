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
    }

    
}
