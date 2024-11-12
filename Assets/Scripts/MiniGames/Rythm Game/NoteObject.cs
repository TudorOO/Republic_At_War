using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    private bool canBePressed;

    private bool obtained = false;
    private SpriteRenderer sp;

    [SerializeField]
    private GameObject hitPrefab, missPrefab, goodPrefab, perfectPrefab;

    [SerializeField]
    private KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress) && canBePressed){
            if(transform.position.y > -3.63){
                Rythm_GameManager.instance.NoteMissed();
                Instantiate(missPrefab, transform.position, Quaternion.identity);
            }else if(transform.position.y > -3.9f || transform.position.y < -4.35f){
                Rythm_GameManager.instance.NormalHit(true);
                Instantiate(hitPrefab, transform.position, Quaternion.identity);
            }else if(transform.position.y > -4f || transform.position.y < -4.25f){
                Rythm_GameManager.instance.GoodHit(true);
                Instantiate(goodPrefab, transform.position, Quaternion.identity);
            }else{
                Rythm_GameManager.instance.PerfectHit(true);
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
                Rythm_GameManager.instance.NoteMissed();
                Instantiate(missPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}