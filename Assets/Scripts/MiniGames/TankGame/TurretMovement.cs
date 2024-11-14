using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement(){
        
            if(Input.GetKey(KeyCode.E)){
            }
    }
}
