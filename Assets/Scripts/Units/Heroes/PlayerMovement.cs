using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField] float jumpSpeed = 30;
    [SerializeField] float fallMultiplier = 1.5f;
    [SerializeField] float lowJumpMultiplier = 4f;
    [SerializeField] float horizontalSpeed = 10;
    [SerializeField] float horizontalAirSpeed = 1.2f;
    [SerializeField]float maxXSpeed = 12;
    [SerializeField]PhysicsMaterial2D noFriction;
    [SerializeField]PhysicsMaterial2D fullFriction;



    float gravity;
    public Vector2 perpendicularToNormalOfSlope {get; set;}
    CapsuleCollider2D myFeetCollider;
    BoxCollider2D myCollider;
    Vector2 colliderSize;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Vector2 moveInput;
    Vector2 vecGravity;

    public bool canWalkOnSlope {get; set;}
    public bool isOnSlope {get; set;}
    public bool canJump {get; set;}
    private bool PlayerHasHorizontalSpeed = false;
    bool isJumping = false;
    bool isAlive = true; //replace with state machine
    
    bool isOnGround = false;
    
    // Start is called before the first frame update
    public void SetSlopeVector(Vector2 normal)
    {
        perpendicularToNormalOfSlope = normal;
    }
    public void SetIsOnSlope(bool isOnSlope)
    {
        this.isOnSlope = isOnSlope;
    }
    
    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        canJump = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeetCollider = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        colliderSize = myCollider.size;
        myRigidbody.freezeRotation = true;
        gravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        CheckGrounded();
        FallDown();
        SwitchMaterialBasedOnSlope();
    }

    private void FallDown()
    {
        if(myRigidbody.velocity.y > 0 && !isJumping)
        {
            myRigidbody.gravityScale = gravity * lowJumpMultiplier;
        }

        if (myRigidbody.velocity.y < 0 )
        {
            myRigidbody.gravityScale = gravity * fallMultiplier;
        }
    }

    private void SwitchMaterialBasedOnSlope()
    {
        if(isOnSlope && (MathF.Abs( myRigidbody.velocity.x) < Mathf.Epsilon) && canWalkOnSlope)
                {
                    myRigidbody.sharedMaterial = fullFriction;
                }
                else
                {
                    myRigidbody.sharedMaterial = noFriction;
                }
    }

    private void CheckGrounded()
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

        if(isOnGround && !isOnSlope )
        {    
            Vector2 playerVelocity = new Vector2(moveInput.x * horizontalSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
        }
            
        if ( isOnGround && isOnSlope  && canWalkOnSlope && !isJumping)
        {
            Vector2 playerVelocity = new Vector2(-moveInput.x * horizontalSpeed * perpendicularToNormalOfSlope.x  , horizontalSpeed * perpendicularToNormalOfSlope.y * -moveInput.x);
            myRigidbody.velocity = playerVelocity;
        }
        
        if(!isOnGround)
        {
            Vector2 playerVelocity = new Vector2(moveInput.x * horizontalAirSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
        }
        else
        {
            myRigidbody.gravityScale = gravity;
        }

    }

    private Vector2 RestrictXSpeed(Vector2 speed)
    {
        if(MathF.Abs( speed.x) > maxXSpeed)
        {
            speed.x = maxXSpeed * MathF.Sign(myRigidbody.velocity.x);
        }
        return speed;
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
            if(!value.isPressed)
            {
                isJumping = false;
            }

            if (!isOnGround || !canJump)
            {
                return;
            }
           

            if(value.isPressed)
            {
                myRigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                isJumping = true;
            }
            
        }
        

}
