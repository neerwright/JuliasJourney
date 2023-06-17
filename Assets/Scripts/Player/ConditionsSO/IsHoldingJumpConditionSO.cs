using UnityEngine;
using Statemachine;

namespace Player
{
	[CreateAssetMenu(menuName = "State Machine/Conditions/Is Holding Jump")]
	public class IsHoldingJumpConditionSO : StateConditionSO<IsHoldingJumpCondition> { }

	public class IsHoldingJumpCondition : Condition
	{
		//Component references
		private PlayerScript _player;
		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
		}

		protected override bool Statement() => _player.jumpInput;
	}
}