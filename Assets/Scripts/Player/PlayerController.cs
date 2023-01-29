using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    [Header("MOVE")] [SerializeField, Tooltip("Raising this value increases collision accuracy at the cost of performance.")]
        private int _freeColliderIterations = 10;
    
        public bool CoyoteUsable{get;set;}
        public float TimeLeftGrounded{get;set;}
        public Vector2 Velocity{get; private set;} 
        //public bool JumpingThisFrame { get; private set; }
        public bool LandingThisFrame { get; private set; }
        public Vector2 VectorAlongSlope => _slopeNormalPerp;
        public bool IsGrounded => _colDown;
        public bool CollisionAbove => _colUp;
        public bool IsNudgingPlayer => _nudgingPlayer;
        public bool IsOnSlopeVertical => _onSlopeVertical;
        public bool IsCompletelyOnSlope => _onSlopeBothRays;
        public bool SlopeInFront => Mathf.Sign(transform.localScale.x) == 1 ? _slopeOnRight : _slopeOnLeft;
        public bool SlopeInBack  => Mathf.Sign(transform.localScale.x) == 1 ? _slopeOnLeft : _slopeOnRight;
        public bool CanWalkOnSlope => _canWalkOnSlope;
        public bool CanUseCoyote => CoyoteUsable && !_colDown && TimeLeftGrounded + _coyoteTimeThreshold > Time.time;
   
        

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

        [SerializeField] private float _coyoteTimeThreshold = 0.1f;



    private Vector2 _lastPosition;
    private Vector2 _targetVelocity;
    private const float MIN_MOVE_DISTANCE = 0.001f;
    private RayRange _raysUp, _raysRight, _raysDown, _raysLeft;
    private bool _colUp, _colRight, _colDown, _colLeft;
    private float _nudgingRaycastOffset = 0.1f;
    private bool _nudgingPlayer = false;
    private bool _onSlopeVertical = false;
    private bool _onSlopeBothRays = false;
    private bool _slopeOnRight = false;
    private bool _slopeOnLeft = false;
    private bool _canWalkOnSlope = false;
    private float _slopeDownAngle;
    private float _slopeSideAngle;
    private Vector2 _slopeNormalPerp;

    private float _rayOffsetY = 0f;
    private float _verticalSlopeCheckOffset = 0.0f;


    private const float NUDGE_MULT = 4f;
    private const float NUDGE_RAY_DIST = 0.4f;
    private const float RAYS_DOWN_RANGE_BONUS = 0.0f;
    private const float NUDGING_SPEED_THRESHOLD = 8f;
    
   

    private Rigidbody2D _rb2d;
    private Player _player;

    void OnEnable() 
    {
        _player = GetComponent<Player>();
        _rb2d = GetComponent<Rigidbody2D>();

        _rayOffsetY =  _characterBounds.size.y /2;

        _verticalSlopeCheckOffset =  _characterBounds.size.x / 2;
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
        RaycastHit2D slopeHitBack  = Physics2D.Raycast(checkPos - new Vector2(_characterBounds.size.x / 2 , 0), -transform.right, _slopeCheckDistanceHorizontal, _groundLayer);
        //Debug.DrawRay(checkPos + new Vector2(_characterBounds.size.x / 2 , 0), transform.right * 100, Color.red);


        if(slopeHitFront)
        {
            Debug.DrawRay(slopeHitFront.point, slopeHitFront.normal, Color.blue);
            _slopeOnRight = true;
            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            _slopeNormalPerp = Vector2.Perpendicular(slopeHitFront.normal).normalized;
        }
        else if(slopeHitBack)
        {
            _slopeOnLeft = true;
            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            _slopeNormalPerp = Vector2.Perpendicular(slopeHitBack.normal).normalized;
        }
        else
        {
            _slopeSideAngle = 0.0f;
            _slopeOnRight = false;
            _slopeOnLeft = false;
        }

        //Hit a wall not a slope
        if(Mathf.Abs(_slopeSideAngle) > 85)
        {
            _slopeOnRight = false;
            _slopeOnLeft = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        _onSlopeBothRays = false;
        
        RaycastHit2D hitLeft = Physics2D.Raycast(checkPos - new Vector2(_verticalSlopeCheckOffset, 0) , Vector2.down, _slopeCheckDistanceVertical, _groundLayer);
        RaycastHit2D hitRight= Physics2D.Raycast(checkPos + new Vector2(_verticalSlopeCheckOffset, 0) , Vector2.down, _slopeCheckDistanceVertical, _groundLayer);

        if (hitLeft || hitRight)
        {
            RaycastHit2D hit;
            if(hitLeft && hitRight) // which one is on a slope?
            {
                float AngleLeft = Vector2.Angle(hitLeft.normal, Vector2.up);
                float AngleRight = Vector2.Angle(hitRight.normal, Vector2.up);

                if( AngleLeft >= AngleRight)
                {
                    hit = hitLeft;    
                }
                else
                {
                    hit = hitRight;
                }

                if (AngleLeft > 0 && AngleRight > 0)
                {
                    _onSlopeBothRays = true;
                }
            } 
            else
            {
                hit = (hitLeft)? hitLeft : hitRight;
            }
            
            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (Mathf.Abs(_slopeDownAngle) > Mathf.Epsilon )
            {
                _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
                _onSlopeVertical = true;
            }
            else  
            {
                _onSlopeVertical = false;
                _onSlopeBothRays = false;
            }


            if (_slopeDownAngle > _maxSlopeAngle || _slopeSideAngle > _maxSlopeAngle)
            {
                _canWalkOnSlope = false;
            }
            else
            {
                _canWalkOnSlope = true;
            }
            
            
            Debug.DrawRay(hit.point, _slopeNormalPerp, Color.red);

        }
        else
        {
            _onSlopeVertical = false;
        }
    }


    #endregion

    #region Move
    private void MoveCharacter(Vector2 move) 
    {
        Debug.DrawRay(_rb2d.position, move * 100, Color.green);
        
        _nudgingPlayer = false;
            
        var pos = _rb2d.position; 
        var furthestPoint = pos + move;

        // check furthest movement. If nothing hit, move and don't do extra checks
        var hit = Physics2D.OverlapBox(furthestPoint, _characterBounds.size, 0, _groundLayer);
        if (!hit) {
            _rb2d.MovePosition(_rb2d.position + move);
            return;
        }


        // otherwise increment away from current pos; see what closest position we can move to
        var positionToMoveTo = _rb2d.position;
        for (int i = 1; i < _freeColliderIterations; i++) {
            // increment to check all but furthestPoint - we did that already
            var t = (float)i / _freeColliderIterations;
            var posToTry = Vector2.Lerp(pos, furthestPoint, t);
            //_rb2d.MovePosition(positionToMoveTo);
            

            if (Physics2D.OverlapBox(posToTry, _characterBounds.size, 0, _groundLayer)) {
                _rb2d.MovePosition(positionToMoveTo); //the last position without a collision
                
                // We've landed on a corner or hit our head on a ledge. Nudge the player gently
                if (i == 1) 
                {
                    //When on a Slope
                    if (IsOnSlopeVertical || SlopeInFront || SlopeInBack)
                    {
    
                        if(_slopeOnLeft || _slopeOnRight)
                        {
                            _rb2d.position = (_rb2d.position - (Vector2.Perpendicular(_slopeNormalPerp) * Time.deltaTime)); //make some space between player and ledge                             
                            _player.movementVector.y = 0;
                            _rb2d.MovePosition(_rb2d.position + _player.movementVector * Time.deltaTime);
                        }
                        else
                        {
                            if(_colDown)
                            {
                                _rb2d.MovePosition(_rb2d.position -  4 * (Vector2.Perpendicular(_slopeNormalPerp) * Time.deltaTime));
                                
                            }
                        }
                        
                    }
                    else
                    {
                        if (!_colDown && !_colLeft && !_colRight && !_colUp && !IsNudgingPlayer)
                        {
                            _rb2d.MovePosition(_rb2d.position + Vector2.up * Time.deltaTime * NUDGE_MULT) ;
                        }
                        
                        //pop up from ground to be able to move forward
                        if(IsGrounded && !_colLeft && !_colRight)
                        {
                            _player.movementVector.y = 0;
                            _rb2d.position = _rb2d.position + Vector2.up * Time.deltaTime * NUDGE_MULT;
                            
                            _rb2d.MovePosition(_rb2d.position + _player.movementVector * Time.deltaTime);
                        }
                            
                        //pop out of wall
                        if(_colRight)
                        {
                            _rb2d.MovePosition(_rb2d.position + Vector2.left  * Time.deltaTime * NUDGE_MULT);
                        }

                        if(_colLeft)
                        {
                            _rb2d.MovePosition(_rb2d.position + Vector2.right  * Time.deltaTime * NUDGE_MULT);
                        }

                        if(_colUp && !IsGrounded)//hit head on plattform
                        {
                            RaycastHit2D leftRay =  Physics2D.Raycast(_rb2d.position + new Vector2(-_nudgeDetectionDistance,_rayOffsetY), Vector2.up, NUDGE_RAY_DIST, _groundLayer);
                            RaycastHit2D rightRay = Physics2D.Raycast(_rb2d.position + new Vector2(_nudgeDetectionDistance,_rayOffsetY), Vector2.up, NUDGE_RAY_DIST, _groundLayer);
                            if(leftRay && rightRay)
                                return;

                            Vector2 dir = new Vector2(0,0);
                            if(rightRay && _player.movementVector.x < -NUDGING_SPEED_THRESHOLD)
                            {
                                _nudgingPlayer = true;
                                dir = Vector2.left * NUDGE_MULT;
                            }
                            if(leftRay && _player.movementVector.x > NUDGING_SPEED_THRESHOLD)
                            {
                                _nudgingPlayer = true;
                                dir = Vector2.right * NUDGE_MULT;
                            }
        
                            _rb2d.position = (_rb2d.position + dir * Time.deltaTime);
                        }
                    }
                    //Check again if w can move after nudging
                    furthestPoint = _rb2d.position + move;
                    var Hit = Physics2D.OverlapBox(furthestPoint, _characterBounds.size, 0, _groundLayer);
                    if (!Hit) {
                        _rb2d.MovePosition(_rb2d.position + move); // keep moving player in desired direction
                        return;
                        }                    
                                                    
                }

                return;
            }
            else
            {
                _rb2d.MovePosition(positionToMoveTo);
            }
            positionToMoveTo = posToTry; //shift one for next iteration
        }
    }


    
    
    #endregion
    
    #region Collisions
    private void RunCollisionChecks() 
    {
        // Generate ray ranges. 
        CalculateRayRanged();

        // Ground
        LandingThisFrame = false;
        var groundedCheck = RunDetection(_raysDown , RAYS_DOWN_RANGE_BONUS);
        if (_colDown && !groundedCheck) 
        {
            TimeLeftGrounded = Time.time; // Only trigger when first leaving
        }
        else if (!_colDown && groundedCheck) {
            CoyoteUsable = true; // Only trigger when first touching
            LandingThisFrame = true;
        }

        _colDown = groundedCheck;

        // The rest
        _colUp = RunDetection(_raysUp, RAYS_DOWN_RANGE_BONUS);
        _colLeft = RunDetection(_raysLeft);
        _colRight = RunDetection(_raysRight);

        bool RunDetection(RayRange range, float extendRange = 0) {
            return EvaluateRayPositions(range).Any(point => Physics2D.Raycast(point, range.Dir, _detectionRayLength + extendRange, _groundLayer));
        }
    }

    private void CalculateRayRanged() 
    {
        var b = new Bounds(transform.position, _characterBounds.size);

        _raysDown = new RayRange(b.min.x + _rayBufferOffset - 0.2f, b.min.y, b.max.x - _rayBufferOffset + 0.2f, b.min.y, Vector2.down);
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
            foreach (var range in new List<RayRange> { _raysRight, _raysLeft }) {
                foreach (var point in EvaluateRayPositions(range)) {
                    Gizmos.DrawRay(point, range.Dir * _detectionRayLength);
                }
            }
            foreach (var point in EvaluateRayPositions(_raysDown)) {
                    Gizmos.DrawRay(point, _raysDown.Dir * (_detectionRayLength + RAYS_DOWN_RANGE_BONUS));
                } 
            foreach (var point in EvaluateRayPositions(_raysUp)) {
                    Gizmos.DrawRay(point, _raysDown.Dir * (_detectionRayLength + RAYS_DOWN_RANGE_BONUS));
                } 
        }

        

        if (!Application.isPlaying) return;

        // Draw the future position. Handy for visualizing gravity
        Gizmos.color = Color.red;
        var move = new Vector3(_player.movementVector.x, _player.movementVector.y) * Time.deltaTime;
        Gizmos.DrawWireCube(transform.position + _characterBounds.center + move, _characterBounds.size);
        
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
