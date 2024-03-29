using UnityEngine;
using Statemachine;

namespace Player
{
	[CreateAssetMenu(menuName = "State Machine/Conditions/Started Moving")]
	public class IsMovingConditionSO : StateConditionSO<IsMovingCondition>
	{
		public float threshold = 0.02f;
	}

	public class IsMovingCondition : Condition
	{
		private PlayerScript _player;
		private IsMovingConditionSO _originSO => (IsMovingConditionSO)base.OriginSO; // The SO this Condition spawned from

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
		}

		protected override bool Statement()
		{
			Vector2 movementVector = _player.movementInput;
			movementVector.y = 0f;
			return movementVector.sqrMagnitude > _originSO.threshold;
		}
	}
}