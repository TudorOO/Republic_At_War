using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSpawner : MonoBehaviour
{
    [SerializeField]
    private PlayerRunning pl;
    int rand;
    [SerializeField]
    private GameObject single;
    [SerializeField]
    private GameObject hit;

    private int last;


    private IEnumerator spawner(){
        while(pl.spawning){
            rand = Random.Range(1, 100);
            if (rand == last){
                rand=rand+1;
            }
            switch(rand % 7){
                case 0:
                    var goat = Instantiate(hit, new Vector2(transform.position.x, 0.8f), Quaternion.identity);//Hitable Line
                    goat.transform.parent = gameObject.transform;
                    break;
                case 1:
                    var goat1 = Instantiate(single, new Vector2(transform.position.x, 3.9f), Quaternion.identity);//Up line
                    goat1.transform.parent = gameObject.transform;
                    break;
                case 2:
                    var goat2 = Instantiate(single, new Vector2(transform.position.x, 1f), Quaternion.identity);//Middle line
                    goat2.transform.parent = gameObject.transform;
                    break;
                case 3:
                    var goat3 = Instantiate(single, new Vector2(transform.position.x, -2f), Quaternion.identity);//Down line
                    goat3.transform.parent = gameObject.transform;
                    break;
                case 4:
                    var goat4 = Instantiate(single, new Vector2(transform.position.x, 3.9f), Quaternion.identity);//Up line
                    var goat5 = Instantiate(single, new Vector2(transform.position.x, 1f), Quaternion.identity);//Middle line
                    goat4.transform.parent = gameObject.transform;
                    goat5.transform.parent = gameObject.transform;
                    break;
                case 5:
                    var goat6 = Instantiate(single, new Vector2(transform.position.x, 3.9f), Quaternion.identity);//Up line
                    var goat7 = Instantiate(single, new Vector2(transform.position.x, -2f), Quaternion.identity);//Down line
                    goat6.transform.parent = gameObject.transform;
                    goat7.transform.parent = gameObject.transform;
                    break;
                case 6:
                    var goat8 = Instantiate(single, new Vector2(transform.position.x, 1f), Quaternion.identity);//Middle line
                    var goat9 = Instantiate(single, new Vector2(transform.position.x, -2f), Quaternion.identity);//Down line
                    goat8.transform.parent = gameObject.transform;
                    goat9.transform.parent = gameObject.transform;
                    break;
            }
            last = rand;
            rand = Random.Range(1, 100);
            if(rand % 10 == 0 && rand % 7 != 0){
                Instantiate(hit, new Vector2(transform.position.x+2f, 0.8f), Quaternion.identity);//Hitable Line
            }
            yield return new WaitForSeconds(0.65f);
        }
    }

    // Update is called once per frame

    public void start(){
        StartCoroutine(spawner());
    }

    void Update()
    {
        
    }
}
