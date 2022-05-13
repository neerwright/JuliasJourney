using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField] float jumpSpeed = 5;
    [SerializeField] float horizontalSpeed = 10;
    [SerializeField] float horizontalAirSpeed = 1.2f;
    [SerializeField]float maxXSpeed = 12;
    [SerializeField]PhysicsMaterial2D noFriction;
    [SerializeField]PhysicsMaterial2D fullFriction;



    public Vector2 perpendicularToNormalOfSlope {get; set;}
    CapsuleCollider2D myFeetCollider;
    BoxCollider2D myCollider;
    Vector2 colliderSize;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Vector2 moveInput;

    public bool canWalkOnSlope {get; set;}
    public bool isOnSlope {get; set;}
    public bool canJump {get; set;}
    public bool PlayerHasHorizontalSpeed = false;
    bool HasJumped = false;
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
        canJump = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeetCollider = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        colliderSize = myCollider.size;
        myRigidbody.freezeRotation = true;
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
        SwitchMaterialBasedOnSlope();
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

        if(HasJumped)
        {
            Debug.Log("is in the air");
            Vector2 playerVelocity = new Vector2(myRigidbody.velocity.x + moveInput.x * horizontalAirSpeed , myRigidbody.velocity.y);
            
            myRigidbody.velocity = RestrictXSpeed(playerVelocity);
        }
        else
        {
            if(isOnGround && !isOnSlope )
            {
            Debug.Log("is ground and not slope");

                
                Vector2 playerVelocity = new Vector2(moveInput.x * horizontalSpeed, myRigidbody.velocity.y);
                myRigidbody.velocity = playerVelocity;
            }
            
            if ( isOnGround && isOnSlope  && canWalkOnSlope)
            {
                Debug.Log("is on slope");
                Vector2 playerVelocity = new Vector2(-moveInput.x * horizontalSpeed * perpendicularToNormalOfSlope.x  , horizontalSpeed * perpendicularToNormalOfSlope.y * -moveInput.x);
                myRigidbody.velocity = playerVelocity;
            }
        }

    }

    private Vector2 RestrictXSpeed(Vector2 speed)
    {
        if(MathF.Abs( speed.x) > maxXSpeed)
        {
            speed.x = maxXSpeed * MathF.Sign(myRigidbody.velocity.x);
        }
        Debug.Log("speed" + speed.x);
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
            if (!isOnGround || !canJump)
            {
                Debug.Log("not on ground");
                return;
            }
           

            if(value.isPressed)
            {
                
                myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
                Debug.Log("space pressed" + myRigidbody.velocity);
                HasJumped = true;

                StartCoroutine(EndJump());
            }
            
        }
        
        IEnumerator EndJump()
        {
            yield return new WaitForSeconds(0.5f);
            HasJumped = false;
        }

}
