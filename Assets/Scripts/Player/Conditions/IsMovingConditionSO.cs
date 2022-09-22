using UnityEngine;

[CreateAssetMenu(menuName = "State Machine/Conditions/Started Moving")]
public class IsMovingConditionSO : StateConditionSO<IsMovingCondition>
{
	public float threshold = 0.02f;
}

public class IsMovingCondition : Condition
{
	private Player _player;
	private new IsMovingConditionSO _originSO => (IsMovingConditionSO)base.OriginSO; // The SO this Condition spawned from

	public override void Awake(StateMachine stateMachine)
	{
		_player = stateMachine.GetComponent<Player>();
	}

	protected override bool Statement()
	{
		Vector2 movementVector = _player.movementInput;
		movementVector.y = 0f;
		return movementVector.sqrMagnitude > _originSO.threshold;
	}
}