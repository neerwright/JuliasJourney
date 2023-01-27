using UnityEngine;


[CreateAssetMenu(menuName = "State Machine/Conditions/Is Character Controller on a Slope")]
public class IsOnSlopeConditionSO : StateConditionSO<IsOnSlopeCondition> { }

public class IsOnSlopeCondition : Condition
{
	private PlayerController _playerController;

	public override void Awake(StateMachine stateMachine)
	{
		_playerController = stateMachine.GetComponent<PlayerController>();
	}

	protected override bool Statement() => _playerController.IsOnSlope || _playerController.SlopeInFront || _playerController.SlopeInBack;
}