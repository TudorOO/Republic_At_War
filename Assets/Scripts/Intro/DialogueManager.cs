using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    private GameObject Image;
    
    public TMP_Text nameText;
    public TMP_Text displayText;   
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    { 
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        if(nameText != null){
            nameText.text = dialogue.name;
        }

        Debug.Log("Conversation with " + dialogue.name);

        sentences.Clear();
        
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }else{
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            Image.GetComponent<imageChanger>().imageID++;
            StartCoroutine(TypeSentence(sentence)); 
        }
    }

    IEnumerator TypeSentence( string sentence ){
        displayText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            displayText.text += letter;
            yield return new WaitForSeconds(0.05f);

        }
    }

    public void EndDialogue(){
        SceneManager.LoadScene("MainWorld");
    }
}
