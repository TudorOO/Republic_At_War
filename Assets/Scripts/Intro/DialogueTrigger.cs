using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool a = true;
    public Dialogue dialogue;
    // Start is called before the first frame update
    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    void Update(){
        if( a == true){
            a = false;
            TriggerDialogue();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
}