using UnityEngine;
using Statemachine;

namespace Player
{
	[CreateAssetMenu(menuName = "State Machine/Conditions/Is Gliding Condition")]
	public class IsGlidingConditionSO : StateConditionSO<IsGlidingCondition> { }

	public class IsGlidingCondition : Condition
	{
		//Component references
		private PlayerController _playerController;

		public override void Awake(StateMachine stateMachine)
		{
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		protected override bool Statement() => _playerController.isGliding;
	}
}