using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{

    public Vector3 masterVolume = new Vector3(0, 0, 0);

    public GameObject gameManager;

    [SerializeField]
    public AudioMixer audioMixer;

    [SerializeField]
    private AudioSource closeMenu;


    [SerializeField]
    private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("isInMenu", 0);
        PlayerPrefs.SetInt("canRunMenu", 1);
        LoadVolume();
    }

    private IEnumerator SetInMenu(){
        yield return new WaitForSeconds(0.3f);
        PlayerPrefs.SetInt("isInMenu", 1);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerPrefs.GetInt("canRunMenu") == 1 && PlayerPrefs.GetInt("isInMenu") == 0){
            menu.SetActive(true);
            StartCoroutine(SetInMenu());
        }
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerPrefs.GetInt("isInMenu") == 1){
            menu.SetActive(false);
            PlayerPrefs.SetInt("isInMenu", 0);
            SaveVolume();
            closeMenu.Play();
        }
    }

    public void SaveVolume(){
        PlayerPrefs.SetFloat("MasterVolume", masterVolume[0]);
        PlayerPrefs.SetFloat("MusicVolume", masterVolume[1]);
        PlayerPrefs.SetFloat("SFXVolume", masterVolume[2]);
    }

    public void LoadVolume(){
        masterVolume = new Vector3(PlayerPrefs.GetFloat("MasterVolume"), PlayerPrefs.GetFloat("MusicVolume"), PlayerPrefs.GetFloat("SFXVolume"));
        UpdateMixer();
    }

    public void UpdateMixer(){
        if(masterVolume[0] == 0){
            audioMixer.SetFloat("MasterParam", -80f);
        }else{
            audioMixer.SetFloat("MasterParam", Mathf.Log10((masterVolume[0]-3f)/50f)*20);
        }

        if(masterVolume[1] == 0){
            audioMixer.SetFloat("MusicParam", -80f);
        }else{
            audioMixer.SetFloat("MusicParam", Mathf.Log10((masterVolume[1]-3f)/50f)*20);
        }
        if(masterVolume[2] == 0){
            audioMixer.SetFloat("FXParam", -80f);
        }else{
            audioMixer.SetFloat("FXParam", Mathf.Log10((masterVolume[2]-3f)/50f)*20);
        }
    }
}
