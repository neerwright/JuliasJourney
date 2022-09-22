using UnityEngine;

[CreateAssetMenu(menuName = "State Machine/Conditions/Is Holding Jump")]
public class IsHoldingJumpConditionSO : StateConditionSO<IsHoldingJumpCondition> { }

public class IsHoldingJumpCondition : Condition
{
	//Component references
    private Player _player;
	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement() => _player.jumpInput;
}