using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to put on bullet prefab that operates it's movement after being shot
public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    // Send bullet in the correct direction
    void Update()
    {
        myRigidbody.velocity = new Vector2 (xSpeed, 0f);
    }

    //If the bullet hits an enemy than destroy the enemy, otherwise destroy the bullet 
    //on any kind of collision
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
