using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;

public class Dialouge : MonoBehaviour
{

    [SerializeField]
    private bool isInCutscene;

    [SerializeField]
    private PlayableDirector director;

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public bool[] IsImageLeft;
    public float textSpeed;


    public GameObject imageComp;
    [SerializeField]
    public GameObject borderRight;
    [SerializeField]
    public GameObject borderLeft;
    public Sprite imageRight;
    public Sprite imageLeft;

    private int index;
    private int conv = 0;

    private RectTransform rt;
    private Image imageSelect;

    void StartDialogue(){
        index = 0;
        PlayerPrefs.SetInt("canRunMenu", 1);
        ChooseLeftOrRight();
        StartCoroutine(TypeLine());
    }

    // Start is called before the first frame update
    void Start()
    {
        rt = imageComp.GetComponent<RectTransform>();
        imageSelect = imageComp.GetComponent<Image>();
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(textComponent.text == lines[index]){
                NextLine();
            }else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    IEnumerator TypeLine(){
        foreach(char c in lines[index].ToCharArray()){
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        if(index < lines.Length - 1){
            index++;
            ChooseLeftOrRight();
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            if(isInCutscene){
                director.Resume();
            }
            PlayerPrefs.SetInt("canRunMenu", 0);
            gameObject.SetActive(false);
        }
    }

    void ChooseLeftOrRight(){
        if(IsImageLeft[index] == true){
                borderRight.SetActive(true);
                borderLeft.SetActive(false);
                rt.transform.localPosition = new Vector3(-780, 230, 0);
                imageSelect.sprite = imageLeft;
            }else{
                borderRight.SetActive(false);
                borderLeft.SetActive(true);
                rt.transform.localPosition = new Vector3(780, 230, 0);
                imageSelect.sprite = imageRight;
            }
    }
}
