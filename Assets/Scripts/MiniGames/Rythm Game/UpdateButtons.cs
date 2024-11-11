using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateButtons : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress)){
            anim.SetTrigger("pressed");
        }
    }
}
