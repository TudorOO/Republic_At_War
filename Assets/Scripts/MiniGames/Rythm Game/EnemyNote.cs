using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNote : MonoBehaviour
{

    private bool canBePressed;

    private bool obtained = false;
    
    private GameObject button;

    private SpriteRenderer sp;

    private int rand;

    [SerializeField]
    private GameObject hitPrefab, missPrefab, goodPrefab, perfectPrefab;

    [SerializeField]
    private KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        switch(keyToPress){
            case KeyCode.W:
                button = GameObject.Find("Button_E_W");
                break;
            case KeyCode.S:
                button = GameObject.Find("Button_E_S");
                break;
            case KeyCode.A:
                button = GameObject.Find("Button_E_A");
                break;
            case KeyCode.D:
                button = GameObject.Find("Button_E_D");
                break;
        }
        sp =GetComponent<SpriteRenderer>();
        sp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(rand >= 1 && canBePressed){
            if(transform.position.y > -3.63 && rand <= 2){
                Instantiate(missPrefab, transform.position, Quaternion.identity);
            }else if((transform.position.y > -3.9f || transform.position.y < -4.35f) && rand <= 5){
                Rythm_GameManager.instance.NormalHit(false);
                Instantiate(hitPrefab, transform.position, Quaternion.identity);
            }else if((transform.position.y > -4f || transform.position.y < -4.25f) && rand <= 8){
                Rythm_GameManager.instance.GoodHit(false);
                Instantiate(goodPrefab, transform.position, Quaternion.identity);
            }else{
                Rythm_GameManager.instance.PerfectHit(false);
                Instantiate(perfectPrefab, transform.position, Quaternion.identity);
            }
            obtained = true;
            gameObject.SetActive(false);
        }
        if(transform.position.y <= -6f){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "activator"){
            rand = Random.Range(1, 10);
            canBePressed = true;    
        }
        if(other.tag == "Enemy"){
            sp.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "activator"){
            canBePressed = false;    
            if(!obtained){
                Instantiate(missPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}