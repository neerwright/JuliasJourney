using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField] float jumpSpeed = 2;
    [SerializeField] float horizontalSpeed = 2;

    Vector2 perpendicularToNormalOfSlope;
    CapsuleCollider2D myFeetCollider;
    BoxCollider2D myCollider;
    Vector2 colliderSize;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Vector2 moveInput;

    bool PlayerHasHorizontalSpeed = false;
    bool HasJumped = false;
    bool isAlive = true; //replace with state machine
    bool isOnSlope = false;
    bool isOnGround = false;
    // Start is called before the first frame update
    public void OnSlope(Vector2 normal)
    {
        isOnSlope = true;
        perpendicularToNormalOfSlope = normal;
    }
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeetCollider = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        colliderSize = myCollider.size;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive)
        {
            return;
        }
        checkGrounded();
        Run();
        FlipSprite();
    }

    private void checkGrounded()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    public void Run()
    {
        

        myAnimator.SetBool("isRunning", PlayerHasHorizontalSpeed);

        
        if(isOnGround && !isOnSlope)
        {
            Vector2 playerVelocity = new Vector2(moveInput.x * horizontalSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
        }
        
        if ( isOnSlope)
        {
            
            Vector2 playerVelocity = new Vector2(-moveInput.x * horizontalSpeed * perpendicularToNormalOfSlope.x  , horizontalSpeed * perpendicularToNormalOfSlope.y * -moveInput.x);
            myRigidbody.velocity = playerVelocity;
        }
        

    }

    void FlipSprite()
    {
        PlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(PlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        
        
    }


    void OnMove(InputValue value)
    {
        if(!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
        {
            if (!isOnGround)
            {
                return;
            }
            else
            {
                HasJumped = false;
            }

            if(value.isPressed)
            {
                
                myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
                HasJumped = true;
            }
        }
}
