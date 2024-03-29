using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Place on prefabs of enemies to make them move
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    Transform enemyTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        enemyTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);

    }

    //collider at front of enemy triggers this event when it bumps into a wall 
    //and turns the enemy around
    private void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    //Changes the direction of the enemy
    void FlipEnemyFacing() 
    {
        enemyTransform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x),1f);
    }
}
