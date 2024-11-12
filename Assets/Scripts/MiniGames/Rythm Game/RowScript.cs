using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowScript : MonoBehaviour
{

    [SerializeField]
    private float beatTempo;


    private int zeroArChance = 25;
    private int oneArChance = 60;
    private int twoArChance = 15;

    // Start is called before the first frame update
    void Start()
    {
        zeroArChance = GetComponentInParent<BeatScroller>().zeroArChance;
        oneArChance = GetComponentInParent<BeatScroller>().oneArChance;
        twoArChance = GetComponentInParent<BeatScroller>().twoArChance;
        beatTempo = GetComponentInParent<BeatScroller>().beatTempo;
        beatTempo = beatTempo / 60f;
        int rand = Random.Range(0, 100);
        if(rand <= zeroArChance && rand >= 0){
            Debug.Log("Empty Row");
            Destroy(this.gameObject);
        }else if(rand <= oneArChance && rand > zeroArChance){
            int ar = Random.Range(0, 4);
            int i = 0;
            Debug.Log("1 Row");
            foreach(Transform child in transform){
                if(i != ar && i != ar+4){
                    Destroy(child.gameObject);
                }
                i++;
            }
        }else if (rand >= 100-twoArChance){
            int ar1 = Random.Range(0, 4);
            int ar2 = Random.Range(0, 4);
            int i = 0;
            Debug.Log("2 Row");
            foreach (Transform child in transform){
                if (i != ar1 && i != ar2 && i != ar1+4 && i != ar2+4){
                    Destroy(child.gameObject);
                }
                i++;
            }
        }else if(rand - oneArChance - twoArChance - zeroArChance>= 0){
            int ar1 = Random.Range(0, 4);
            int i = 0;
            Debug.Log("3 Row");
            foreach(Transform child in transform){
                if(i == ar1 || i == ar1+4){
                    Destroy(child.gameObject);
                }
                i++;
            }
        }else{
            int ar = Random.Range(0, 4);
            int i = 0;
            foreach(Transform child in transform){
                if(i != ar && i != ar+4){
                    Destroy(child.gameObject);
                }
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount <= 0){
            Destroy(this.gameObject);
        }
        transform.position -= new Vector3(0f, beatTempo * Time.deltaTime * GetComponentInParent<BeatScroller>().virusMultifier, 0f);
    }
}
