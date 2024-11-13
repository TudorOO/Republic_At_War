using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

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
        transform.position = new Vector2(transform.position.x + 0.001f, transform.position.y);
        xClimb += 0.001f;
        if(xClimb >= 15.5f){
            xClimb = 0;
            transform.position = new Vector2(startX, transform.position.y);
        }
    }
}
