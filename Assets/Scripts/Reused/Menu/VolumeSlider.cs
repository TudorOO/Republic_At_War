using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private int index;
    public int yup;

    private string start;
    void Start(){
        start = GetComponent<TMP_Text>().text;
        GetComponent<TMP_Text>().text = start + " < " + (int)gameManager.GetComponent<VolumeManager>().masterVolume[index] + " >";
    }
    
    void Update(){
        GetComponent<TMP_Text>().text = start + " < " + (int)gameManager.GetComponent<VolumeManager>().masterVolume[index] + " >";
    }
}
