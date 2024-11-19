using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

    [SerializeField]
    private float speed = 0.001f;

    private float xClimb;
    private float startX;

    // Start is called before the first frame update
    void Start()
    {
        xClimb = 0f;
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        xClimb += speed;
        if(xClimb >= 15.5f){
            xClimb = 0;
            transform.position = new Vector2(startX, transform.position.y);
        }
    }
}
