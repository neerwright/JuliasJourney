

using UnityEngine;


[CreateAssetMenu(menuName = "State Machine/Conditions/Is Character Controller on a too steep of a Slope")]
public class IsOnSteepSlopeConditionSO : StateConditionSO<IsOnSteepSlopeCondition> { }

public class IsOnSteepSlopeCondition : Condition
{
	private PlayerController _playerController;

	public override void Awake(StateMachine stateMachine)
	{
		_playerController = stateMachine.GetComponent<PlayerController>();
	}

	protected override bool Statement() => !_playerController.CanWalkOnSlope && (_playerController.IsOnSlopeVertical) && _playerController.IsGrounded && (_playerController.SlopeInFront || _playerController.SlopeInBack); 
}