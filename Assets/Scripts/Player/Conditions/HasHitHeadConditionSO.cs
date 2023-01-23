using UnityEngine;


[CreateAssetMenu(menuName = "State Machine/Conditions/Has Hit the Head")]
public class HasHitHeadConditionSO : StateConditionSO<HasHitHeadCondition> { }

public class HasHitHeadCondition : Condition
{
	//Component references
	private Player _player;
	private PlayerController _playerController;
	private Transform _transform;

	public override void Awake(StateMachine stateMachine)
	{
		_transform = stateMachine.GetComponent<Transform>();
		_player = stateMachine.GetComponent<Player>();
		_playerController = stateMachine.GetComponent<PlayerController>();
	}

	protected override bool Statement()
	{
		bool isMovingUpwards = _player.movementVector.y > 0f;
		
		if (isMovingUpwards)
		{
			
			if(_playerController.CollisionAbove && !_playerController.IsNudgingPlayer)
			{
				_player.jumpInput = false;
				_player.movementVector.y = 0f;
				

				return true;
			}
		}

		return false;
	}
}