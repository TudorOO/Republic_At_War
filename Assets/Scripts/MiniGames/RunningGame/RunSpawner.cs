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
                    Instantiate(hit, new Vector2(transform.position.x, 0.8f), Quaternion.identity);//Hitable Line
                    break;
                case 1:
                    Instantiate(single, new Vector2(transform.position.x, 3.9f), Quaternion.identity);//Up line
                    break;
                case 2:
                    Instantiate(single, new Vector2(transform.position.x, 1f), Quaternion.identity);//Middle line
                    break;
                case 3:
                    Instantiate(single, new Vector2(transform.position.x, -2f), Quaternion.identity);//Down line
                    break;
                case 4:
                    Instantiate(single, new Vector2(transform.position.x, 3.9f), Quaternion.identity);//Up line
                    Instantiate(single, new Vector2(transform.position.x, 1f), Quaternion.identity);//Middle line
                    break;
                case 5:
                    Instantiate(single, new Vector2(transform.position.x, 3.9f), Quaternion.identity);//Up line
                    Instantiate(single, new Vector2(transform.position.x, -2f), Quaternion.identity);//Down line
                    break;
                case 6:
                    Instantiate(single, new Vector2(transform.position.x, 1f), Quaternion.identity);//Middle line
                    Instantiate(single, new Vector2(transform.position.x, -2f), Quaternion.identity);//Down line
                    break;
            }
            last = rand;
            rand = Random.Range(1, 100);
            if(rand % 10 == 0){
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
