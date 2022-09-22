using UnityEngine;

[CreateAssetMenu(fileName = "ApplyMovementVector", menuName = "State Machine/Actions/Apply Movement Vector")]
public class ApplyMovementVectorActionSO : StateActionSO<ApplyMovementVectorAction> { }

public class ApplyMovementVectorAction : StateAction
{
	//Component references
    private Player _player;
	private PlayerController _playerController;

	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
		_playerController = stateMachine.GetComponent<PlayerController>();
	}

	public override void OnUpdate()
	{
		_playerController.Move(_player.movementVector * Time.deltaTime);//applies the current movementVector
		//_player.movementVector = _characterController.velocity;
	}
}