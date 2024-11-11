using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackElem : MonoBehaviour
{
    public float speed = 0.07f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        if(transform.position.x <= -10f){
            GameObject.Destroy(transform.gameObject);
        }   
    }
}
