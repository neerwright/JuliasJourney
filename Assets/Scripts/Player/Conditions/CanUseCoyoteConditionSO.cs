using UnityEngine;

[CreateAssetMenu(menuName = "State Machine/Conditions/Can use Coyote Jump")]
public class CanUseCoyoteConditionSO : StateConditionSO<CanUseCoyoteCondition> { }

public class CanUseCoyoteCondition : Condition
{
	//Component references
	private PlayerController _playerController;

	public override void Awake(StateMachine stateMachine)
	{
		_playerController = stateMachine.GetComponent<PlayerController>();
	}

	protected override bool Statement() => _playerController.CanUseCoyote;
}