using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;    
using UnityEngine.Audio;
using TMPro;

public class Menu : MonoBehaviour
{
    private int select;

    [SerializeField]
    private GameObject Button_1;
    [SerializeField]
    private GameObject Button_2;
    [SerializeField]
    private GameObject Button_3;
    [SerializeField]
    private GameObject Button_4;

    [SerializeField]
    private GameObject volumeManager;


    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private string exitScene;

    [SerializeField]
    private AudioSource audioSourceFX;
    [SerializeField]
    private AudioSource audioSourceFXM;

    void Start(){
        select = 1;
    }

    void Update(){
        switch(select){
            case 1:
                Button_1.GetComponent<TMP_Text>().color = Color.yellow;
                Button_2.GetComponent<TMP_Text>().color = Color.white;
                Button_3.GetComponent<TMP_Text>().color = Color.white;
                Button_4.GetComponent<TMP_Text>().color = Color.white;
                break;
            case 2:
                Button_1.GetComponent<TMP_Text>().color = Color.white;
                Button_2.GetComponent<TMP_Text>().color = Color.yellow;
                Button_3.GetComponent<TMP_Text>().color = Color.white;
                Button_4.GetComponent<TMP_Text>().color = Color.white;
                break;
            case 3:
                Button_1.GetComponent<TMP_Text>().color = Color.white;
                Button_2.GetComponent<TMP_Text>().color = Color.white;
                Button_3.GetComponent<TMP_Text>().color = Color.yellow;
                Button_4.GetComponent<TMP_Text>().color = Color.white;
                break;
            case 4:
                Button_1.GetComponent<TMP_Text>().color = Color.white;
                Button_2.GetComponent<TMP_Text>().color = Color.white;
                Button_3.GetComponent<TMP_Text>().color = Color.white;
                Button_4.GetComponent<TMP_Text>().color = Color.yellow;
                break;
        }

        if(select > 0 && select < 5){
            if(Input.GetKeyDown(KeyCode.S) && select != 4){
                audioSourceFXM.Play();
                select++;
                //audio
            }
            if(Input.GetKeyDown(KeyCode.W) && select != 1){
                audioSourceFXM.Play();
                select--;
                //audio
            }
            if(Input.GetKeyDown(KeyCode.D)){
                switch(select){
                    case 1:
                        if(volumeManager.GetComponent<VolumeManager>().masterVolume[0] < 100){
                            volumeManager.GetComponent<VolumeManager>().masterVolume[0] +=5;
                            audioSourceFXM.Play();
                            volumeManager.GetComponent<VolumeManager>().UpdateMixer();
                        }
                        break;
                    case 2:
                        if(volumeManager.GetComponent<VolumeManager>().masterVolume[1] < 100){
                            volumeManager.GetComponent<VolumeManager>().masterVolume[1] +=5;
                            audioSourceFXM.Play();
                            volumeManager.GetComponent<VolumeManager>().UpdateMixer();
                        }
                        break;
                    case 3:
                        if(volumeManager.GetComponent<VolumeManager>().masterVolume[2] < 100){
                            volumeManager.GetComponent<VolumeManager>().masterVolume[2] +=5;
                            audioSourceFXM.Play();
                            volumeManager.GetComponent<VolumeManager>().UpdateMixer();
                        }
                        break;
                    }
                }
            if(Input.GetKeyDown(KeyCode.A)){
                switch(select){
                    case 1:
                        if(volumeManager.GetComponent<VolumeManager>().masterVolume[0] < 100){
                            volumeManager.GetComponent<VolumeManager>().masterVolume[0] -=5;
                            audioSourceFXM.Play();
                            volumeManager.GetComponent<VolumeManager>().UpdateMixer();
                        }
                        break;
                    case 2:
                        if(volumeManager.GetComponent<VolumeManager>().masterVolume[1] < 100){
                            volumeManager.GetComponent<VolumeManager>().masterVolume[1] -=5;
                            audioSourceFXM.Play();
                            volumeManager.GetComponent<VolumeManager>().UpdateMixer();
                        }
                        break;
                    case 3:
                        if(volumeManager.GetComponent<VolumeManager>().masterVolume[2] < 100){
                            volumeManager.GetComponent<VolumeManager>().masterVolume[2] -=5;
                            audioSourceFXM.Play();
                            volumeManager.GetComponent<VolumeManager>().UpdateMixer();
                        }
                        break;
                    }
                }
            
            if (Input.GetKeyDown(KeyCode.Return) && select == 4){
                volumeManager.GetComponent<VolumeManager>().SaveVolume();
                PlayerPrefs.SetInt("isInMenu", 0);
                SceneManager.LoadScene(exitScene);
            }
        }
    }
}
