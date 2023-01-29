using UnityEngine;

[CreateAssetMenu(fileName = "SlopeSlideDown", menuName = "State Machine/Actions/Slope Slide down")]
public class SlopeSlideDownActionSO : StateActionSO<SlopeSlideDownAction>
{
	[Tooltip("Speed multiplyer")]
	public float speed = 8f;
	
}

public class SlopeSlideDownAction : StateAction
{
	//Component references
	private Player _player;
	private PlayerController _playerController;
	private new SlopeSlideDownActionSO _originSO => (SlopeSlideDownActionSO)base.OriginSO; // The SO this StateAction spawned from
	
	

	
	
	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
		_playerController = stateMachine.GetComponent<PlayerController>();
	}

	public override void OnUpdate()
	{
		Vector2 SlopeVector = _playerController.VectorAlongSlope;
		
		_player.movementVector.x = SlopeVector.x * _originSO.speed;
		_player.movementVector.y = SlopeVector.y * _originSO.speed;					
	}
	
}