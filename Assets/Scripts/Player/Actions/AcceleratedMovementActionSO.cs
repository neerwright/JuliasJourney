using UnityEngine;

[CreateAssetMenu(fileName = "HorizontalAcceleratedMovement", menuName = "State Machine/Actions/Horizontal accelerated Movement")]
public class AcceleratedMovementActionSO : StateActionSO<AcceleratedMovementAction>
{
	[Tooltip("Maximum speed")]
	public float maxSpeed = 5f;

	[Tooltip("Horizontal acceleration")]
	public float acceleration = 3f;

	[Tooltip("time it takes for the acceleration to get dampened")]
	[SerializeField]
	[Range(0,1)]
	public float damping = 0f;
}

public class AcceleratedMovementAction : StateAction
{
	//Component references
	private Player _player;
	private new AcceleratedMovementActionSO _originSO => (AcceleratedMovementActionSO)base.OriginSO; // The SO this StateAction spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
	}

	public override void OnUpdate()
	{
		_player.movementVector.y = 0f;
		//acceleration --> delta.Time is used 
		_player.movementVector.x += _player.movementInput.x  * _originSO.acceleration * Time.deltaTime;
		//Decelerate
		_player.movementVector.x *= Mathf.Pow(1f - _originSO.damping, Time.deltaTime * 10f);
		
		
		//MaxSpeed
		_player.movementVector.x = Mathf.Clamp(_player.movementVector.x, -_originSO.maxSpeed, _originSO.maxSpeed);
	}
}