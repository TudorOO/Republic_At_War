using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

   private void Update(){
    if(Input.GetKey(KeyCode.A) && transform.position.x >= -17f){
        transform.position = new Vector3(transform.position.x - moveSpeed, 0f, -10f);
    }
    if(Input.GetKey(KeyCode.D) && transform.position.x <= 19f){
        transform.position = new Vector3(transform.position.x + moveSpeed, 0f, -10f);
    }
   }
} 