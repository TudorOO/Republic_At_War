using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    //[SerializeField]
    //private float beatTempo;

    public bool isSpawning;

    public int zeroArChance = 25;
    public int oneArChance = 60;
    public int twoArChance = 15;

    public int beatTempo = 120;
    [SerializeField]
    private GameObject RowPrefab;

    public float spawnTime = 0.5f;

    public float virusMultifier = 1;

    public float travelSpeed = 3.2f;

    private float RowDistance = 1.3f;

    public bool isRunning;

    private void spawnRow(){
        GameObject a = Instantiate(RowPrefab) as GameObject;
        a.transform.parent = gameObject.transform;
        a.transform.localPosition = new Vector3(0, travelSpeed+2f, 0);
    }

    private IEnumerator RowSpawner(){
        while(isRunning && PlayerPrefs.GetInt("isInMenu") == 0){
            isSpawning = true;
            yield return new WaitForSeconds(spawnTime);
            spawnRow();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isSpawning = false;
       // beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRunning){
            isSpawning = false;
        }else{
            //transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
            if(isSpawning == false){
                StartCoroutine(RowSpawner());
            }
        }
    }
}
