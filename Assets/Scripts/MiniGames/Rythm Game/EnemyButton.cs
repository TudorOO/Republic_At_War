using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButton : MonoBehaviour
{

    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FlashButton(){
        anim.SetTrigger("pressed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
