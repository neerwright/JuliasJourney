using UnityEngine;

[CreateAssetMenu(fileName = "SlopeMovement", menuName = "State Machine/Actions/Slope Movement")]
public class SlopeMovementActionSO : StateActionSO<SlopeMovementAction>
{
	[Tooltip("Maximum speed")]
	public float maxSpeed = 5f;
	[Tooltip("Speed multiplyer")]
	public float speed = 8f;
	
}

public class SlopeMovementAction : StateAction
{
	//Component references
	private Player _player;
	private PlayerController _playerController;
	private new SlopeMovementActionSO _originSO => (SlopeMovementActionSO)base.OriginSO; // The SO this StateAction spawned from
	
	private bool _useSlopeMovement = false;
	private float _angleCorrection;
	private const float BONUS_ANGLE_DOWN = 0.6f;
	private const float BONUS_ANGLE_UP = -0.7f;
	private const float TRANSITION_ROUGHNESS_MULTIPLIER = 4.5f;
	
	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
		_playerController = stateMachine.GetComponent<PlayerController>();
	}

	public override void OnUpdate()
	{
		_useSlopeMovement = false;
		if( _playerController.CanWalkOnSlope)
		{
			_angleCorrection = 0;
			CheckMovementAndCalculateAngle();

			if(_useSlopeMovement)
			{
				Vector2 SlopeVector = _playerController.VectorAlongSlope;
				_player.movementVector.x = SlopeVector.x * -_player.movementInput.x * _originSO.speed;
				_player.movementVector.y = SlopeVector.y * -_player.movementInput.x * _originSO.speed + _angleCorrection;			
			}
			else
			{
				_player.movementVector.y = 0;
				_player.movementVector.x = _player.movementInput.x * _originSO.speed;
			}
	
		}
		//MaxSpeed
		_player.movementVector.x = Mathf.Clamp(_player.movementVector.x, -_originSO.maxSpeed, _originSO.maxSpeed);	 
		
	}
	private void CheckMovementAndCalculateAngle()
	{
		if (_playerController.IsOnSlopeVertical && _playerController.SlopeInBack)
		{
			_useSlopeMovement = true;

			if (Mathf.Abs(_player.movementInput.x) > Mathf.Epsilon)
				_angleCorrection = BONUS_ANGLE_DOWN;
		}

		//smooth out transition when in front of a slope and starting to walk up
		if (_playerController.SlopeInFront && !_playerController.IsOnSlopeVertical)
		{
			_useSlopeMovement = true;
			if (Mathf.Abs(_player.movementInput.x) > Mathf.Epsilon)
				_angleCorrection = Vector2.Dot(_playerController.VectorAlongSlope, Vector2.up) * TRANSITION_ROUGHNESS_MULTIPLIER;
				
			if(_angleCorrection > 0) // stay more grounded until slope is under our feet
				_angleCorrection = -_angleCorrection;
		}
		
		if (_playerController.SlopeInFront && _playerController.IsOnSlopeVertical)
		{
			_useSlopeMovement = true;

			if (Mathf.Abs(_player.movementInput.x) > Mathf.Epsilon)
				_angleCorrection = BONUS_ANGLE_UP;
		}
		
		//smooth out transition when we start walking down a slope
		if(_playerController.IsCompletelyOnSlope && !_playerController.SlopeInFront && !_playerController.SlopeInBack)
		{
			_useSlopeMovement = true;

			if (Mathf.Abs(_player.movementInput.x) > Mathf.Epsilon)
				_angleCorrection = -0.7f;
		}


	}
}