using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    LayerMask groundLayer;
    LayerMask climbingLayer;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (20f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float startingGravity;
    bool isAlive = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("Ground");
        climbingLayer = LayerMask.GetMask("Climbing");
        startingGravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }
    }

    void OnMove(InputValue value)
    {
        if(isAlive){
            moveInput = value.Get<Vector2>();
        }
    }

    void OnJump(InputValue value)
    {   
        if(isAlive) {
            if(value.isPressed && myFeetCollider.IsTouchingLayers(groundLayer))
            {
                myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            }
        }
    }

    void OnFire(InputValue value)
    {
        if(isAlive) {
            // what we are spawning and where we are spawning it
            Instantiate(bullet, gun.position, transform.rotation);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * playerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        myAnimator.SetBool("isRunning", CheckHorzMovement());
    }

    void FlipSprite() 
    {

        if(CheckHorzMovement())
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x),1f);
        }
    }

    void ClimbLadder() 
    {
        if(myFeetCollider.IsTouchingLayers(climbingLayer))
        {
            Vector2 playerVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y*climbSpeed);
            myRigidbody.velocity = playerVelocity;
            myRigidbody.gravityScale = 0f;
            myAnimator.SetBool("isClimbing", CheckVertMovement());
        }
        else{
            myRigidbody.gravityScale = startingGravity;
            myAnimator.SetBool("isClimbing", false);
        }
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private bool CheckHorzMovement()
    {
        return Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    }

    private bool CheckVertMovement()
    {
        return Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
    }

}
