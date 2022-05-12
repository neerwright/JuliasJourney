using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveUpSlopes : MonoBehaviour
{
    
    [SerializeField] float slopeCheckDistance = 0.2f;
    [SerializeField] float test = 0.2f;
    float slopeDownAngle;
    float slopeDownAngleOld;
    bool isTouchingGround = false;
    bool isOnSlope;
    BoxCollider2D myFeetCollider;
    Vector2 colliderSize;
    Vector2 colliderOffset;
    Vector2 PerpendicularToNormalOfSlope;
    Renderer myRenderer;
    PlayerMovement myMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        myFeetCollider = GetComponent<BoxCollider2D>();
        colliderSize = myFeetCollider.size; 
        colliderOffset = myFeetCollider.offset;
        myRenderer = GetComponent<Renderer>();
        myMovementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        SlopeCheck();
    }

    private void SlopeCheck()
    {
        
        Vector2 checkPos = transform.position + CalculatePositionOfFeet();
        SlopeCheckVertical(checkPos);
    }
    private Vector3 CalculatePosition()
    {
        return new Vector3(0.0f, -colliderSize.y / 2);
    }
    
    private Vector3 CalculatePositionOfFeet()
    {
        Debug.Log( Mathf.Sin((transform.rotation.eulerAngles.z)* Mathf.Deg2Rad));
        float xPositionAfterRotation = Mathf.Sin(transform.rotation.eulerAngles.z* Mathf.Deg2Rad) * (myRenderer.bounds.size.x / 2  );
        //float yPositionAfterRotation = -colliderSize.y / 2;
        float yPositionAfterRotation = Mathf.Cos(transform.rotation.eulerAngles.z* Mathf.Deg2Rad)* -(myRenderer.bounds.size.y / 2 -1.6f );
        return new Vector3(xPositionAfterRotation, yPositionAfterRotation);
    }

    private void SlopeCheckHorizontal(Vector2 checkPosition)
    {
        
    }
    private void SlopeCheckVertical(Vector2 checkPosition)
    {
       
        RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, slopeCheckDistance, LayerMask.GetMask("Ground"));
        
        
        if (hit)
        {
            PerpendicularToNormalOfSlope = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            Debug.DrawRay(checkPosition, hit.point - checkPosition , Color.green);
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            Debug.DrawRay(hit.point, PerpendicularToNormalOfSlope, Color.red);

            if(slopeDownAngle != slopeDownAngleOld)
            {
                Debug.Log("is on slope");
                isOnSlope = true;
                myMovementScript.OnSlope(PerpendicularToNormalOfSlope);
            }

            slopeDownAngleOld = slopeDownAngle;
        }
    }
    private void CheckGround()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isTouchingGround = true;
        }
        else
        {
            isTouchingGround = false;
        }
        
    }
}
