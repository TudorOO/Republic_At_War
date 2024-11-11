using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class MainMenuInput : MonoBehaviour
{
    public bool IntroPlayed = false;
    public Vector3 masterVolume;
    private bool switching = false;




    [SerializeField]
    private int select = 0;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private AudioSource audioSourceFX;
    [SerializeField]
    private AudioSource audioSourceFXM;
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Animator PlanetAnim;


    [SerializeField]
    private GameObject EnterText;
    [SerializeField]
    private GameObject ReqText;   


    [SerializeField]
    private GameObject Button_1;
    [SerializeField]
    private GameObject Button_2;
    [SerializeField]
    private GameObject Button_3;
    [SerializeField]
    private GameObject Button_4;

    [SerializeField]
    private GameObject Button_5;
    [SerializeField]
    private GameObject Button_6;
    [SerializeField]
    private GameObject Button_7;
    [SerializeField]
    private GameObject Button_8;

    void Start(){
        if(PlayerPrefs.HasKey("MasterVolume")){
            LoadVolume();
        }else{
            masterVolume = new Vector3(50f, 50f, 50f);
        }
    }

    public IEnumerator LoadNextStuff()
    {
        Debug.Log("Starting Coroutine[Switching Scene]");
        yield return new WaitForSeconds(0.5f);
        PlayerPrefs.DeleteAll();
        SaveVolume();
        UpdateMixer();
        Debug.Log("Switching...");
        SceneManager.LoadScene("Intro");
    }




    // Update is called once per frame
    void Update()
    {
        UpdateMixer();
        if(switching == false){
        if(Input.GetKeyDown(KeyCode.Return) && select == 0){
            audioSourceFX.Play();
            PlanetAnim.SetTrigger("toLeft");
            EnterText.SetActive(false);
           // ReqText.SetActive(false);  
            Button_1.GetComponent<Animator>().SetTrigger("goLeft");
            Button_2.GetComponent<Animator>().SetTrigger("goLeft");
            Button_3.GetComponent<Animator>().SetTrigger("goLeft");
            Button_4.GetComponent<Animator>().SetTrigger("goLeft");
            StartCoroutine(waitForMenu());
            select = -1;
        }
        if(!PlayerPrefs.HasKey("currentLevel")){
            Button_2.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.gray;
        }
        switch(select){
            case 1:
                Button_1.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.yellow;
                if(PlayerPrefs.HasKey("currentLevel")){
                    Button_2.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                }
                Button_3.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_4.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                break;
            case 2:
                Button_1.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                if(PlayerPrefs.HasKey("currentLevel")){
                    Button_2.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.yellow;
                }
                Button_3.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_4.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                break;
            case 3:
                Button_1.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                if(PlayerPrefs.HasKey("currentLevel")){
                    Button_2.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                }
                Button_3.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.yellow;
                Button_4.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                break;
            case 4:
                Button_1.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                if(PlayerPrefs.HasKey("currentLevel")){
                    Button_2.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                }
                Button_3.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_4.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.yellow;
                break;
            case 5:
                Button_5.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.yellow;
                Button_6.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_7.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_8.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                break;
            case 6:
                Button_5.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_6.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.yellow;
                Button_7.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_8.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                break;
            case 7:
                Button_5.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_6.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_7.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.yellow;
                Button_8.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                break;
            case 8:
                Button_5.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_6.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_7.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.white;
                Button_8.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = Color.yellow;
                break;
        }

        if(select > 0 && select < 5){
            if(Input.GetKeyDown(KeyCode.S) && select != 4){
                audioSourceFXM.Play();
                if(!PlayerPrefs.HasKey("currentLevel") && select == 1){
                    select+=2;
                }else{
                    select++;
                }
                //audio
            }
            if(Input.GetKeyDown(KeyCode.W) && select != 1){
                audioSourceFXM.Play();
                if(!PlayerPrefs.HasKey("currentLevel") && select == 3){
                    select-=2;
                }else{
                    select--;
                }
                //audio
            }
            if(Input.GetKeyDown(KeyCode.Escape)){
                audioSourceFX.Play();
                SaveVolume();
                Debug.Log("Exiting application...");
                Application.Quit();
            }
            if(Input.GetKeyDown(KeyCode.Return)){
                if(select == 1){
                    audioSourceFX.Play();
                    Debug.Log("Switchin..");
                    switching = true;
                    StartCoroutine(LoadNextStuff());
                }else{
                    switch(select){
                        /*case 1:
                            audioSourceFX.Play();
                            Debug.Log("Switchin..");
                            waitForSoundEffect();
                            SceneManager.LoadScene("MainWorld");
                            break;*/
                        case 2:
                            audioSourceFX.Play();
                            StartCoroutine(ContinueGame());
                            break;
                        case 3:
                            audioSourceFX.Play();
                            raiseOptions();
                            select = 5;
                            break;
                        case 4:
                            audioSourceFX.Play();
                            SaveVolume();
                            Debug.Log("Exiting application...");
                            Application.Quit();
                            break;
                    }
                }
            }
        }else if(select >=5){
            if(Input.GetKeyDown(KeyCode.W) && select != 5){
                audioSourceFXM.Play();
                select--;
            }
            if(Input.GetKeyDown(KeyCode.S) && select != 8){
                audioSourceFXM.Play();
                select++;
            }
            if ((Input.GetKeyDown(KeyCode.Return) && select == 8) || Input.GetKeyDown(KeyCode.Escape)){
                audioSourceFX.Play();
                SaveVolume();
                raiseMenu();
                select = 1;
            }
            if(Input.GetKeyDown(KeyCode.D)){
                switch(select){
                    case 5:
                        if(masterVolume[0] < 100){
                            masterVolume[0] +=5;
                            audioSourceFXM.Play();
                            UpdateMixer();
                        }
                        break;
                    case 6:
                        if(masterVolume[1] < 100){
                            masterVolume[1] +=5;
                            audioSourceFXM.Play();
                            UpdateMixer();
                        }
                        break;
                    case 7:
                        if(masterVolume[2] < 100){
                            masterVolume[2] +=5;
                            audioSourceFXM.Play();
                            UpdateMixer();
                        }
                        break;
                }
            }
            if(Input.GetKeyDown(KeyCode.A)){
                switch(select){
                    case 5:
                        if(masterVolume[0] > 0){
                            masterVolume[0] -=5;
                            audioSourceFXM.Play();
                            UpdateMixer();
                        }
                        break;
                    case 6:
                        if(masterVolume[1] > 1){
                            masterVolume[1] -=5;
                            audioSourceFXM.Play();
                            UpdateMixer();
                        }
                        break;
                    case 7:
                        if(masterVolume[2] > 1){
                            masterVolume[2] -=5;
                            audioSourceFXM.Play();
                            UpdateMixer();
                        }
                        break;
                }
            }
        }
        }

    }
    private IEnumerator waitForMenu()
    {
        yield return new WaitForSeconds(1);
        select = 1;
    }

    void raiseOptions()
    {
        Button_1.SetActive(false);
        Button_2.SetActive(false);
        Button_3.SetActive(false);
        Button_4.SetActive(false);
        Button_5.SetActive(true);
        Button_6.SetActive(true);
        Button_7.SetActive(true);
        Button_8.SetActive(true);
    }
    void raiseMenu()
    {
        Button_1.SetActive(true);
        Button_2.SetActive(true);
        Button_3.SetActive(true);
        Button_4.SetActive(true);
        Button_5.SetActive(false);
        Button_6.SetActive(false);
        Button_7.SetActive(false);
        Button_8.SetActive(false);

    }

    private IEnumerator ContinueGame()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MainWorld");
    }

    void UpdateMixer(){
        if(masterVolume[0] <= 0){
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

    void SaveVolume(){
        PlayerPrefs.SetFloat("MasterVolume", masterVolume[0]);
        PlayerPrefs.SetFloat("MusicVolume", masterVolume[1]);
        PlayerPrefs.SetFloat("SFXVolume", masterVolume[2]);
    }

    void LoadVolume(){
        masterVolume = new Vector3(PlayerPrefs.GetFloat("MasterVolume"), PlayerPrefs.GetFloat("MusicVolume"), PlayerPrefs.GetFloat("SFXVolume"));
        UpdateMixer();
    }


}
