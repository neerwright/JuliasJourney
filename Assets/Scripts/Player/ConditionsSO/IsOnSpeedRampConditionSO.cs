using UnityEngine;
using Statemachine;

namespace Player
	{

	[CreateAssetMenu(menuName = "State Machine/Conditions/Is Character On Speed Ramp?")]
	public class IsOnSpeedRampConditionSO : StateConditionSO<IsOnSpeedRampCondition> { }

	public class IsOnSpeedRampCondition : Condition
	{
		private PlayerController _playerController;

		public override void Awake(StateMachine stateMachine)
		{
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		protected override bool Statement() => (_playerController.SlopeInFront || _playerController.IsOnSlopeVertical) && _playerController.SlopeTag == "SpeedRamp";
	}
}