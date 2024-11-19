using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterGameManager : MonoBehaviour
{
    [SerializeField]
    private int state;
    public float waitTime = 7f;
    public float damage;
    private float delay = 0;

    private bool clicked = false;
    private bool isOut = false;

    public Animator anim;

    public Transform[] states;

    private int rand;
    private float rnd;

    private void Start(){
        SwitchState();
    }

    private void SwitchState(){
        rand = Random.Range(0, 8);
        state = rand;
        transform.position = states[state].position;
        anim.SetTrigger("fadeIn");

        StartCoroutine(Attack());
    }

    public void OnMouseDown(){
        if(isOut){
            clicked = true;
            isOut = false;
            Debug.Log("Clicked");
        }
    }

    private IEnumerator Attack(){
        delay = 0;
        isOut = true;
        while(delay < waitTime){
            if(!clicked){
            yield return new WaitForSeconds(0.5f);
            delay+=0.5f;
            }else{
                delay = waitTime +1f;
            }
        }
        if(clicked){
            isOut = false;
            clicked = false;
            anim.SetTrigger("fadeOut");
            yield return new WaitForSeconds(0.5f);
            //DealDamage
            rnd = Random.Range(0f, 8f);
            yield return new WaitForSeconds(rnd);
            SwitchState();
        }else{
            isOut = false;
            //TakeDamage
            //anim.SetTrigger("Atack");
            yield return new WaitForSeconds(0.3f);
            SwitchState();
        }
    }
}
