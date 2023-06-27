using UnityEngine;
using Statemachine;

namespace Player
{
	[CreateAssetMenu(menuName = "State Machine/Conditions/is Rewinding")]
	public class IsRewindingConditionSO : StateConditionSO<IsRewindingCondition> { }

	public class IsRewindingCondition : Condition
	{
		//Component references
		private PlayerController _playerController;
		public override void Awake(StateMachine stateMachine)
		{
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		protected override bool Statement() => _playerController.IsRewinding;
	}
}