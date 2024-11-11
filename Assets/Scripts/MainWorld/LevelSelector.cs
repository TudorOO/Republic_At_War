using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelLeft, levelRight, levelUp, levelDown;
    public string LevelScene;
    public bool isUnlocked = false;
    public bool isExtra = false;

    void Start(){
        if(transform.name == "Levelb1" || transform.name == "Levelb2"){
            isExtra = true;
        }
    }

    void Update(){
        if(isUnlocked){
            GetComponent<SpriteRenderer>().color = Color.green;
        }else{
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
