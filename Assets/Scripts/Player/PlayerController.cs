using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    [Header("MOVE")] [SerializeField, Tooltip("Raising this value increases collision accuracy at the cost of performance.")]
        private int _freeColliderIterations = 10;
    
        public Vector2 Velocity{get; private set;}
        public bool JumpingThisFrame { get; private set; }
        public bool LandingThisFrame { get; private set; }
        public bool IsGrounded => _colDown;
    
   

    [Header("COLLISION")] 
        [SerializeField] private Bounds _characterBounds;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private int _detectorCount = 3;
        [SerializeField] private float _detectionRayLength = 0.1f;
        [SerializeField] [Range(0.1f, 0.3f)] private float _rayBuffer = 0.1f; // Prevents side detectors hitting the ground

    private Vector2 _lastPosition;
    private Vector2 _targetVelocity;
    private const float MIN_MOVE_DISTANCE = 0.001f;
    private RayRange _raysUp, _raysRight, _raysDown, _raysLeft;
    private bool _colUp, _colRight, _colDown, _colLeft;
    private float _timeLeftGrounded;
    
   
    //private List<RaycastHit2D> hitBuffer = new List<RaycastHit2D>(16);

    private Rigidbody2D _rb2d;
    private Player _player;

    void OnEnable() 
    {
        _player = GetComponent<Player>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    //Passed parameter needs to have deltaTime applied 
    public void Move(Vector2 movementVector)
    {
        RunCollisionChecks();
        CheckForWalls();
        MoveCharacter(movementVector);
    }

    #region WallCheck

    private void CheckForWalls()
    {
        if (_player.movementVector.x > 0 && _colRight || _player.movementVector.x < 0 && _colLeft) 
        {
            // Don't walk through walls
            _player.movementVector.x = 0;
        }
    }
    #endregion

    #region Move
    private void MoveCharacter(Vector2 move) 
    {
            var pos = _rb2d.position; 
            var furthestPoint = pos + move;

            // check furthest movement. If nothing hit, move and don't do extra checks
            var hit = Physics2D.OverlapBox(furthestPoint, _characterBounds.size, 0, _groundLayer);
            if (!hit) {
                _rb2d.position += move;
                return;
            }

            // otherwise increment away from current pos; see what closest position we can move to
            var positionToMoveTo = _rb2d.position;
            for (int i = 1; i < _freeColliderIterations; i++) {
                // increment to check all but furthestPoint - we did that already
                var t = (float)i / _freeColliderIterations;
                var posToTry = Vector2.Lerp(pos, furthestPoint, t);

                if (Physics2D.OverlapBox(posToTry, _characterBounds.size, 0, _groundLayer)) {
                    _rb2d.position = positionToMoveTo; //the last position without a collision

                    // We've landed on a corner or hit our head on a ledge. Nudge the player gently
                    if (i == 1) {
                        if (_player.movementVector.y < 0) _player.movementVector.y = 0;
                        
                        //nudge player --> but he will slide on ground
                        //Vector2 hitPosition = new Vector2(hit.transform.position.x, hit.transform.position.y);
                        //var dir = _rb2d.position - hitPosition;
                        //_rb2d.position += dir.normalized * move.magnitude;
                    }

                    return;
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
        // This is crying out for some kind of refactor. 
        var b = new Bounds(transform.position, _characterBounds.size);

        _raysDown = new RayRange(b.min.x + _rayBuffer, b.min.y, b.max.x - _rayBuffer, b.min.y, Vector2.down);
        _raysUp = new RayRange(b.min.x + _rayBuffer, b.max.y, b.max.x - _rayBuffer, b.max.y, Vector2.up);
        _raysLeft = new RayRange(b.min.x, b.min.y + _rayBuffer, b.min.x, b.max.y - _rayBuffer, Vector2.left);
        _raysRight = new RayRange(b.max.x, b.min.y + _rayBuffer, b.max.x, b.max.y - _rayBuffer, Vector2.right);
    }


    private IEnumerable<Vector2> EvaluateRayPositions(RayRange range) 
    {
        for (var i = 0; i < _detectorCount; i++) {
            var t = (float)i / (_detectorCount - 1);
            yield return Vector2.Lerp(range.Start, range.End, t);
        }
    }
    #endregion
    
    #region Gizmo

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

            // Draw the future position. Handy for visualizing gravity
            Gizmos.color = Color.red;
            var move = new Vector3(_player.movementVector.x, _player.movementVector.y) * Time.deltaTime;
            Gizmos.DrawWireCube(transform.position + _characterBounds.center + move, _characterBounds.size);
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
