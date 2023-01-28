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

	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
		_playerController = stateMachine.GetComponent<PlayerController>();
	}

	public override void OnUpdate()
	{
		if( _playerController.CanWalkOnSlope)
		{
			if((_playerController.IsOnSlope && _playerController.SlopeInBack) || _playerController.SlopeInFront)
			{
				float angleCorrection = 0;
				if(_playerController.SlopeInFront && Mathf.Abs(_player.movementInput.x) > Mathf.Epsilon)
				{
					angleCorrection = -0.5f;
				}
				if(_playerController.SlopeInBack && Mathf.Abs(_player.movementInput.x) > Mathf.Epsilon)
				{
					angleCorrection = 0.6f;
				}

				Vector2 SlopeVector = _playerController.VectorAlongSlope;
				_player.movementVector.x = SlopeVector.x * -_player.movementInput.x * _originSO.speed;
				_player.movementVector.y = SlopeVector.y * -_player.movementInput.x * _originSO.speed + angleCorrection;
			}
			else
			{
				Debug.Log("vert " + _playerController.IsOnSlope);
				Debug.Log("back " + _playerController.SlopeInBack);				
				_player.movementVector.x = _player.movementInput.x * _originSO.speed;
			}

			

			
		}
		//MaxSpeed
		_player.movementVector.x = Mathf.Clamp(_player.movementVector.x, -_originSO.maxSpeed, _originSO.maxSpeed);	 
		
	}
}