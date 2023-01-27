using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    [Header("MOVE")] [SerializeField, Tooltip("Raising this value increases collision accuracy at the cost of performance.")]
        private int _freeColliderIterations = 10;
    
        public Vector2 Velocity{get; private set;}
        public Vector2 AngleAlongSlope => _slopeNormalPerp;
        public bool JumpingThisFrame { get; private set; }
        public bool LandingThisFrame { get; private set; }
        public bool IsGrounded => _colDown;
        public bool CollisionAbove => _colUp;
        public bool IsNudgingPlayer => _nudgingPlayer;
        public bool IsOnSlope => _onSlope;
        public bool CanWalkOnSlope => _canWalkOnSlope;
   

    [Header("COLLISION")] 
        [SerializeField] private Bounds _characterBounds;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private int _detectorCount = 3;
        [SerializeField] private float _detectionRayLength = 0.1f;
        [SerializeField] [Range(0.1f, 0.3f)] private float _rayBufferOffset = 0.1f; // Prevents side detectors hitting the ground
        [SerializeField] private float _nudgeDetectionDistance = 0.7f;
        [SerializeField] private float _slopeCheckDistanceVertical = 0.7f;
        [SerializeField] private float _slopeCheckDistanceHorizontal = 0.7f;
        [SerializeField] private float _maxSlopeAngle = 60.0f;


    private Vector2 _lastPosition;
    private Vector2 _targetVelocity;
    private const float MIN_MOVE_DISTANCE = 0.001f;
    private RayRange _raysUp, _raysRight, _raysDown, _raysLeft;
    private bool _colUp, _colRight, _colDown, _colLeft;
    private float _timeLeftGrounded;
    private float _nudgingRaycastOffset = 0.1f;
    private bool _nudgingPlayer = false;
    private bool _onSlope = false;
    private bool _canWalkOnSlope = false;
    private float _slopeDownAngle;
    private float _slopeSideAngle;
    private float _slopeDownAngleOld;
    private Vector2 _slopeNormalPerp;

    float rayOffsetX =0;
    float rayOffsetY =0;


    private const float NUDGE_MULT = 10f;

    private Rigidbody2D _rb2d;
    private Player _player;

    void OnEnable() 
    {
        _player = GetComponent<Player>();
        _rb2d = GetComponent<Rigidbody2D>();

        //ti start the raycast on top of player for nudging
        rayOffsetX = (_characterBounds.size.x /2 + _nudgingRaycastOffset);
        rayOffsetY =  _characterBounds.size.y /2;

    }

    //Passed parameter needs to have deltaTime applied 
    public void Move(Vector2 movementVector)
    {
        RunCollisionChecks();
        CheckForWalls(ref movementVector);
        SlopeCheck();
        MoveCharacter(movementVector);
    }

    #region WallAndSlopeCheck

    private void CheckForWalls(ref Vector2 movementVector)
    {
        if(IsOnSlope)
            return;

        if (movementVector.x > 0 && _colRight || movementVector.x < 0 && _colLeft) 
        {
            // Don't walk through walls
            movementVector.x = 0;  
        }

        
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, _characterBounds.size.y / 2);
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
        

    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos + new Vector2(_characterBounds.size.x / 2 , 0), transform.right, _slopeCheckDistanceHorizontal, _groundLayer);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos - new Vector2(_characterBounds.size.x / 2 , 0), -transform.right, _slopeCheckDistanceHorizontal, _groundLayer);
        Debug.DrawRay(checkPos + new Vector2(_characterBounds.size.x / 2 , 0), transform.right, Color.red);


        if(slopeHitFront)
        {
            Debug.DrawRay(slopeHitFront.point, slopeHitFront.normal, Color.blue);
            _onSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            _slopeNormalPerp = Vector2.Perpendicular(slopeHitFront.normal).normalized;
        }
        else if(slopeHitBack)
        {
            _onSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            _slopeNormalPerp = Vector2.Perpendicular(slopeHitBack.normal).normalized;
        }
        else
        {
            _slopeSideAngle = 0.0f;
            _onSlope = false;
        }

        //Hit a wall not a slope
        if(Mathf.Abs(_slopeSideAngle) > 85)
        {
            _onSlope = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _slopeCheckDistanceVertical, _groundLayer);
        if (hit)
        {
            
            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (Mathf.Abs(_slopeDownAngle) > Mathf.Epsilon )
            {
                _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
                _onSlope = true;
            }
            else if (Mathf.Abs(_slopeSideAngle) <= Mathf.Epsilon) //if we detected slope horizontaly, dont overwrite _onSlope or _slopeNormalPerp
            {
                _onSlope = false;
            }


            if (_slopeDownAngle > _maxSlopeAngle || _slopeSideAngle > _maxSlopeAngle)
            {
                _canWalkOnSlope = false;
            }
            else
            {
                _canWalkOnSlope = true;
            }
            
            //_slopeDownAngleOld = _slopeDownAngle;
            
            //Debug.DrawRay(hit.point, _slopeNormalPerp, Color.red);
            //Debug.DrawRay(hit.point, hit.normal, Color.blue);

        }
    }


    #endregion

    #region Move
    private void MoveCharacter(Vector2 move) 
    {
 
        Debug.DrawRay(_rb2d.position, move * 100, Color.green);   
        var pos = _rb2d.position; 
        var furthestPoint = pos + move;

        // check furthest movement. If nothing hit, move and don't do extra checks
        var hit = Physics2D.OverlapBox(furthestPoint, _characterBounds.size, 0, _groundLayer);
        if (!hit) {
            _rb2d.position += move;
            return;
        }
        Debug.Log("hit");
        Vector2 dir = new Vector2(0,0);
        dir = CheckForNudging(_rb2d.position);

        // otherwise increment away from current pos; see what closest position we can move to
        var positionToMoveTo = _rb2d.position;
        for (int i = 1; i < _freeColliderIterations; i++) {
            // increment to check all but furthestPoint - we did that already
            var t = (float)i / _freeColliderIterations;
            var posToTry = Vector2.Lerp(pos, furthestPoint, t);

            

            if (Physics2D.OverlapBox(posToTry, _characterBounds.size, 0, _groundLayer)) {
                _rb2d.position = positionToMoveTo; //the last position without a collision
                
                
                //hit head onto plattform    
                if(IsNudgingPlayer)
                {
                    Debug.Log("nuuud");
                    _rb2d.position += dir.normalized * move.magnitude;    
                }
                // We've landed on a corner or hit our head on a ledge. Nudge the player gently
                if (i == 1) 
                {

                    //trying to jump on an edge
                    if (!_colDown && !_colLeft && !_colRight && !_colUp)
                    {
                        _rb2d.position += Vector2.up * Time.deltaTime * NUDGE_MULT ;
                    }
                    
                    //pop up from ground to be able to move
                    if (!IsOnSlope)
                    {
                        if(IsGrounded && !_colLeft && !_colRight)
                        {
                            _player.movementVector.y = 0;
                            _rb2d.position += Vector2.up * Time.deltaTime * NUDGE_MULT ;
                        }
                        
                        //pop out of wall
                        if(_colRight)
                        {
                            _player.movementVector.x = 0;
                            _rb2d.position += Vector2.left  * Time.deltaTime * NUDGE_MULT;
                        }

                        if(_colLeft)
                        {
                            _player.movementVector.x = 0;
                            _rb2d.position += Vector2.right  * Time.deltaTime * NUDGE_MULT;
                        }
                    }
                    
                    
                    
                    
                }

                return;
            }

            positionToMoveTo = posToTry; //shift one for next iteration
        }
    }


    private Vector2 CheckForNudging(Vector2 center)
    {
        Vector2 dir = new Vector2(0,0);
        
        //nudge player when hitting his Head on a platform above
        RaycastHit2D leftRay =  Physics2D.Raycast(new Vector2 (center.x -rayOffsetX, center.y + rayOffsetY) , Vector2.up, 0.1f, _groundLayer);
        RaycastHit2D rightRay = Physics2D.Raycast(new Vector2 (center.x + rayOffsetX, center.y + rayOffsetY) , Vector2.up, 0.1f, _groundLayer);
        
        //did we hit our head on the left side (and not close to the middle?)
        _nudgingPlayer = false;
        
        if(leftRay )
        {
            RaycastHit2D MiddleRay =  Physics2D.Raycast(new Vector2 (leftRay.point.x + _nudgeDetectionDistance , center.y + rayOffsetY) , Vector2.up, 0.1f, _groundLayer);

            if(!MiddleRay )
            {
                
                dir = _rb2d.position - leftRay.point;
                _nudgingPlayer = true;
            }
            else
            {
                _nudgingPlayer = false;
            }
            

        }
        else if(rightRay)
        {
            RaycastHit2D MiddleRay =  Physics2D.Raycast(new Vector2 (rightRay.point.x - _nudgeDetectionDistance , center.y + rayOffsetY) , Vector2.up, 0.1f, _groundLayer);

            if(!MiddleRay)
            {
                if(Application.isPlaying)
                {
                    Debug.DrawRay( center + new Vector2 (_characterBounds.size.x/2,_characterBounds.size.y/2), Vector2.up, Color.green, 1.0f );
                }
                dir = _rb2d.position - rightRay.point;
                _nudgingPlayer = true;
            }
            else
            {
                _nudgingPlayer = false;
            }
            
        }
        
        return dir;
    }
    
    #endregion
    
    #region Collisions
    private void RunCollisionChecks() 
    {
        // Generate ray ranges. 
        CalculateRayRanged();

        // Ground
        LandingThisFrame = false;
        var groundedCheck = RunDetection(_raysDown);
        if (_colDown && !groundedCheck) _timeLeftGrounded = Time.time; // Only trigger when first leaving
        else if (!_colDown && groundedCheck) {
            //_coyoteUsable = true; // Only trigger when first touching
            LandingThisFrame = true;
        }

        _colDown = groundedCheck;

        // The rest
        _colUp = RunDetection(_raysUp);
        _colLeft = RunDetection(_raysLeft);
        _colRight = RunDetection(_raysRight);

        bool RunDetection(RayRange range) {
            return EvaluateRayPositions(range).Any(point => Physics2D.Raycast(point, range.Dir, _detectionRayLength, _groundLayer));
        }
    }

    private void CalculateRayRanged() 
    {
        var b = new Bounds(transform.position, _characterBounds.size);

        _raysDown = new RayRange(b.min.x + _rayBufferOffset, b.min.y, b.max.x - _rayBufferOffset, b.min.y, Vector2.down);
        _raysUp = new RayRange(b.min.x + _rayBufferOffset, b.max.y, b.max.x - _rayBufferOffset, b.max.y, Vector2.up);
        _raysLeft = new RayRange(b.min.x, b.min.y + _rayBufferOffset, b.min.x, b.max.y - _rayBufferOffset, Vector2.left);
        _raysRight = new RayRange(b.max.x, b.min.y + _rayBufferOffset, b.max.x, b.max.y - _rayBufferOffset, Vector2.right);
    }


    private IEnumerable<Vector2> EvaluateRayPositions(RayRange range) 
    {
        for (var i = 0; i < _detectorCount; i++) {
            var t = (float)i / (_detectorCount - 1);
            yield return Vector2.Lerp(range.Start, range.End, t);
        }
    }
    #endregion
    
    #region Debug

    private void OnDrawGizmos() 
    {     
         

        // Bounds
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + _characterBounds.center, _characterBounds.size);

        // Rays
        if (!Application.isPlaying) {
            CalculateRayRanged();
            Gizmos.color = Color.blue;
            foreach (var range in new List<RayRange> { _raysUp, _raysRight, _raysDown, _raysLeft }) {
                foreach (var point in EvaluateRayPositions(range)) {
                    Gizmos.DrawRay(point, range.Dir * _detectionRayLength);
                }
            }
        }

        

        if (!Application.isPlaying) return;
        
        DrawNudgingRays();
        // Draw the future position. Handy for visualizing gravity
        Gizmos.color = Color.red;
        var move = new Vector3(_player.movementVector.x, _player.movementVector.y) * Time.deltaTime;
        Gizmos.DrawWireCube(transform.position + _characterBounds.center + move, _characterBounds.size);

        //if(IsOnSlope) 
        //    Debug.Log("is On slope: ");      

        //if(IsGrounded)
        //    Debug.Log("grounded ");
        

        
    }

    private void DrawNudgingRays()
    {
        if(_nudgingPlayer && _colUp)
        {
            Vector2 position = new Vector2();
            position.x = transform.position.x;
            position.y = transform.position.y;
            Debug.DrawRay(position + new Vector2 (_characterBounds.size.x/2 + _nudgingRaycastOffset,_characterBounds.size.y/2), Vector2.up, Color.green, 1.0f );
            Debug.DrawRay(position - new Vector2 (_characterBounds.size.x/2 + _nudgingRaycastOffset, - _characterBounds.size.y/2), Vector2.up, Color.green, 1.0f );
        }
        
    }

    #endregion

}



public struct RayRange {
    public RayRange(float x1, float y1, float x2, float y2, Vector2 dir) {
        Start = new Vector2(x1, y1);
        End = new Vector2(x2, y2);
        Dir = dir;
    }

    public readonly Vector2 Start, End, Dir;
}
