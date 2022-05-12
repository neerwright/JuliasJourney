using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveUpSlopes : MonoBehaviour
{
    
    [SerializeField] float slopeCheckDistance = 0.75f;
    

    float slopeDownAngle;
    float slopeDownAngleOld;
    float slopeSideAngle;

    //bool isTouchingGround = false;
    

    CapsuleCollider2D myFeetCollider;
    Vector2 colliderSize;
    Vector2 colliderOffset;
    Vector2 PerpendicularToNormalOfSlope;
    Renderer myRenderer;
    PlayerMovement myMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        myFeetCollider = GetComponent<CapsuleCollider2D>();
        colliderSize = myFeetCollider.size; 
        colliderOffset = myFeetCollider.offset;
        myRenderer = GetComponent<Renderer>();
        myMovementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        SlopeCheck();
    }

    private void SlopeCheck()
    {
        
        Vector2 checkPos = transform.position + CalculatePositionOfFeet();
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }
    private Vector3 CalculatePosition()
    {
        return new Vector3(0.0f, -colliderSize.y / 2);
    }
    
    private Vector3 CalculatePositionOfFeet()
    {
        float xPositionAfterRotation = Mathf.Sin(transform.rotation.eulerAngles.z* Mathf.Deg2Rad) * (myRenderer.bounds.size.x / 2  );
        //float yPositionAfterRotation = -colliderSize.y / 2;
        float yPositionAfterRotation = Mathf.Cos(transform.rotation.eulerAngles.z* Mathf.Deg2Rad)* -(myRenderer.bounds.size.y / 2  );
        return new Vector3(xPositionAfterRotation, yPositionAfterRotation);
    }

    private void SlopeCheckHorizontal(Vector2 checkPosition)
    {
        //ajust position of beam a little bit more upwards
        checkPosition.y -= -0.2f;
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPosition, transform.right, slopeCheckDistance, LayerMask.GetMask("Ground"));
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPosition, -transform.right, slopeCheckDistance, LayerMask.GetMask("Ground"));
        Debug.DrawRay(checkPosition, transform.right , Color.green);
        
        if (slopeHitFront)
        {
            PerpendicularToNormalOfSlope = Vector2.Perpendicular(slopeHitFront.normal).normalized;
            myMovementScript.SetSlopeVector(PerpendicularToNormalOfSlope);
            Debug.Log("HitFront");
            myMovementScript.SetIsOnSlope(true);
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            PerpendicularToNormalOfSlope = Vector2.Perpendicular(slopeHitBack.normal).normalized;
            myMovementScript.SetSlopeVector(PerpendicularToNormalOfSlope);
            Debug.Log("HitBack");
            myMovementScript.SetIsOnSlope(true);
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            myMovementScript.SetIsOnSlope(false);
        }
    }
    private void SlopeCheckVertical(Vector2 checkPosition)
    {
        //ajust position of beam a little bit more upwards
        checkPosition.y -= -1.6f;
        RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, slopeCheckDistance * 8, LayerMask.GetMask("Ground"));
        
        
        if (hit)
        {
            PerpendicularToNormalOfSlope = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            //Debug.DrawRay(checkPosition, hit.point - checkPosition , Color.green);
            //Debug.DrawRay(hit.point, hit.normal, Color.blue);
            //Debug.DrawRay(hit.point, PerpendicularToNormalOfSlope, Color.red);

            if(slopeDownAngle != slopeDownAngleOld)
            {
                
                myMovementScript.SetSlopeVector(PerpendicularToNormalOfSlope);
                myMovementScript.SetIsOnSlope(true);
            }

            slopeDownAngleOld = slopeDownAngle;
        }
    }
    
}
