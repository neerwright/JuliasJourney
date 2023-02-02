using UnityEngine;
using Statemachine;

namespace Player
{
	[CreateAssetMenu(menuName = "State Machine/Conditions/Is Character Controller Grounded")]
	public class IsGroundedConditionSO : StateConditionSO<IsGroundedCondition> { }

	public class IsGroundedCondition : Condition
	{
		private PlayerController _playerController;

		public override void Awake(StateMachine stateMachine)
		{
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		protected override bool Statement() => _playerController.IsGrounded;
	}
}