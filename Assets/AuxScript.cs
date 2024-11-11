using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuxScript : MonoBehaviour
{
    public bool beat = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Changed bool");
            GameManager.beatLevel = true;
            SceneManager.LoadScene("MainWorld");
        }
    }
}
