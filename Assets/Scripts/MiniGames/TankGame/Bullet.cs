using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 5;
    public int maxDistance = 10;

    private Vector2 startPosition;
    private float conquaredDistance = 0f;
    private Rigidbody2D rb;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(){
        startPosition = transform.position;
        rb.velocity = transform.up * speed;
    }

    private void Update(){
        conquaredDistance = Vector2.Distance(transform.position, startPosition);
        if(conquaredDistance > maxDistance){
            DisableObject();
        }
    }

    private void DisableObject()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collided");
        DisableObject();
    }
}
