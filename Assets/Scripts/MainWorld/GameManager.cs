using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject currentLevel;
    private GameObject nextLevel1;

    public static bool beatLevel = false;
    public int levelsBeated;

    public bool canRunMenu = true;

    [SerializeField]
    private AudioSource goNext;
    [SerializeField]
    private AudioSource EnterLevel;

    public string currentLevelPrefs;

    private GameObject nextLevel;

    public void LoadLevels(){
        int i = 0;
        Debug.Log("LOADING N LEVELS FROM 1: " + PlayerPrefs.GetInt("levelsBeat"));
        GameObject currentLevel = GameObject.Find("Level1");
        while(i < PlayerPrefs.GetInt("levelsBeat")){

            nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelDown;
            if(nextLevel1 != null && nextLevel1.GetComponent<LevelSelector>().isUnlocked == false){
                 nextLevel1.GetComponent<LevelSelector>().isUnlocked = true;
                 if(nextLevel1.GetComponent<LevelSelector>().isExtra != true){
                    currentLevel = nextLevel1;
                }
             }

            nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelUp;
            if(nextLevel1 != null && nextLevel1.GetComponent<LevelSelector>().isUnlocked == false){
                nextLevel1.GetComponent<LevelSelector>().isUnlocked = true;
                if(nextLevel1.GetComponent<LevelSelector>().isExtra != true){
                    currentLevel = nextLevel1;
                }
            }

            nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelRight;
            if(nextLevel1 != null && nextLevel1.GetComponent<LevelSelector>().isUnlocked == false){
                nextLevel1.GetComponent<LevelSelector>().isUnlocked = true;
                if(nextLevel1.GetComponent<LevelSelector>().isExtra != true){
                    currentLevel = nextLevel1;
                }
            }

            nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelLeft;
            if(nextLevel1 != null && nextLevel1.GetComponent<LevelSelector>().isUnlocked == false){
                nextLevel1.GetComponent<LevelSelector>().isUnlocked = true;
                if(nextLevel1.GetComponent<LevelSelector>().isExtra != true){
                    currentLevel = nextLevel1;
                }
            }
            i++;
        }
    }

    private bool checkIfLast(GameObject currentLevel){
        int last = 0;
        LevelSelector nextLevelSelector;

        nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelDown;
        if(nextLevel1 != null){
            Debug.Log("Found Down");
            nextLevelSelector = nextLevel1.GetComponent <LevelSelector>();
            Debug.Log(nextLevelSelector.isUnlocked + "   " + nextLevelSelector.isExtra);
            if(nextLevel1 != null && nextLevelSelector.isUnlocked == false && nextLevelSelector.isExtra == false){
                last++;
            }
        }

        nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelUp;
        if(nextLevel1 != null){
            Debug.Log("Found Up");
            nextLevelSelector = nextLevel1.GetComponent <LevelSelector>();
            Debug.Log(nextLevelSelector.isUnlocked + "   " + nextLevelSelector.isExtra);
            if(nextLevel1 != null && nextLevelSelector.isUnlocked == false && nextLevelSelector.isExtra == false){
                last++;
            }
        }

        nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelRight;
        if(nextLevel1 != null){
            Debug.Log("Found Right");
            nextLevelSelector = nextLevel1.GetComponent <LevelSelector>();
            Debug.Log(nextLevelSelector.isUnlocked + "   " + nextLevelSelector.isExtra);
            if(nextLevel1 != null && nextLevelSelector.isUnlocked == false && nextLevelSelector.isExtra == false){
                last++;
            }
        }

        nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelLeft;
        if(nextLevel1 != null){
            Debug.Log("Found Left");
            nextLevelSelector = nextLevel1.GetComponent <LevelSelector>();
            Debug.Log(nextLevelSelector.isUnlocked + "   " + nextLevelSelector.isExtra);
            if(nextLevel1 != null && nextLevelSelector.isUnlocked == false && nextLevelSelector.isExtra == false){
                last++;
            }
        }
        Debug.Log(last);
        if(last > 0){
            return true;
        }else{
            return false;
        }
    }
    
    private IEnumerator goToLevel(){
        yield return new WaitForSeconds(0.4f);
        Debug.Log("Loading Scene: " + currentLevel.GetComponent<LevelSelector>().LevelScene);
        PlayerPrefs.SetString("currentLevel", currentLevel.name);
        Debug.Log("Loading Scene: " + currentLevel.GetComponent<LevelSelector>().LevelScene);
        SceneManager.LoadScene(currentLevel.GetComponent<LevelSelector>().LevelScene);
    }
    
    void Start()
    {
        PlayerPrefs.SetInt("isInMenu", 0);
        PlayerPrefs.SetInt("canRunMenu", 1);
        if (!PlayerPrefs.HasKey("currentLevel")){
            Debug.Log("PlayerPrefs: CurrentLevel set to " + currentLevel.name);
            PlayerPrefs.SetString("currentLevel", currentLevel.name);
            PlayerPrefs.SetInt("levelsBeat", 0);
            transform.position = currentLevel.transform.position;
        }else{
            LoadLevels();
            currentLevel = GameObject.Find(PlayerPrefs.GetString("currentLevel"));
            if(PlayerPrefs.GetInt("UnlockedB1") == 1){
                GameObject.Find("Levelb1").GetComponent<LevelSelector>().isUnlocked = true;
            }
            transform.position = currentLevel.transform.position;
            if(beatLevel == true){
                if(currentLevel.name == "Levelb2"){
                    GameObject.Find("Levelb1").GetComponent<LevelSelector>().isUnlocked = true;
                    PlayerPrefs.SetInt("UnlockedB1", 1);
                }
                if(currentLevel.name == "Levelb1"){
                    PlayerPrefs.SetInt("BeatB1", 1);
                }
                if(checkIfLast(currentLevel)){
                    Debug.Log("Last level beat!");
                    PlayerPrefs.SetInt("levelsBeat", PlayerPrefs.GetInt("levelsBeat")+1);
                    beatLevel = false;

                    nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelDown;
                    if(nextLevel1 != null){
                        Debug.Log(nextLevel1.name + "  ESTE JOS");
                        nextLevel1.GetComponent<LevelSelector>().isUnlocked = true;
                    }

                    nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelUp;
                    if(nextLevel1 != null){
                        Debug.Log(nextLevel1.name + "  ESTE SUS");
                        nextLevel1.GetComponent<LevelSelector>().isUnlocked = true;
                    }

                    nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelRight;
                    if(nextLevel1 != null){
                        Debug.Log(nextLevel1.name + "  ESTE LA DREAPTA");
                        nextLevel1.GetComponent<LevelSelector>().isUnlocked = true;
                    }

                    nextLevel1 = currentLevel.GetComponent<LevelSelector>().levelLeft;
                    if(nextLevel1 != null){
                        Debug.Log(nextLevel1.name + "  ESTE LA STANGA");
                        nextLevel1.GetComponent<LevelSelector>().isUnlocked = true;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentLevelPrefs = PlayerPrefs.GetString("currentLevel");
        levelsBeated = PlayerPrefs.GetInt("levelsBeat");
        if(PlayerPrefs.GetInt("isInMenu") == 0){
            if(Input.GetKeyDown(KeyCode.Return)){
                EnterLevel.Play();
                StartCoroutine(goToLevel());
            }
            if(Input.GetKeyDown(KeyCode.D)){
                nextLevel = currentLevel.GetComponent<LevelSelector>().levelRight;
                Debug.Log("Tried");
                if(nextLevel != null && nextLevel.GetComponent<LevelSelector>().isUnlocked != false){
                    goNext.Play();
                    transform.position = nextLevel.transform.position;
                    currentLevel = nextLevel;
                }
            }else if(Input.GetKeyDown(KeyCode.A)){
                nextLevel = currentLevel.GetComponent<LevelSelector>().levelLeft;
                if(nextLevel != null && nextLevel.GetComponent<LevelSelector>().isUnlocked != false){
                    goNext.Play();
                    transform.position = nextLevel.transform.position;
                    currentLevel = nextLevel;
                }
            }else if(Input.GetKeyDown(KeyCode.S)){
                nextLevel = currentLevel.GetComponent<LevelSelector>().levelDown;
                if(nextLevel != null && nextLevel.GetComponent<LevelSelector>().isUnlocked != false){
                    goNext.Play();
                    transform.position = nextLevel.transform.position;
                    currentLevel = nextLevel;
                }
            }else if(Input.GetKeyDown(KeyCode.W)){
                nextLevel = currentLevel.GetComponent<LevelSelector>().levelUp;
                if(nextLevel != null && nextLevel.GetComponent<LevelSelector>().isUnlocked != false){
                    goNext.Play();
                    transform.position = nextLevel.transform.position;
                    currentLevel = nextLevel;
                }
            }
        }
    }
}
