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
			Vector2 SlopeAngle = _playerController.AngleAlongSlope;
			_player.movementVector.x = SlopeAngle.x * -_player.movementInput.x * _originSO.speed;
			_player.movementVector.y = SlopeAngle.y * -_player.movementInput.x * _originSO.speed;
			_player.movementVector += Vector2.up * Time.deltaTime * 100;
			//MaxSpeed
			_player.movementVector.x = Mathf.Clamp(_player.movementVector.x, -_originSO.maxSpeed, _originSO.maxSpeed);
		}
		
		 
		
	}
}