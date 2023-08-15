using UnityEngine;
using Statemachine;

namespace Player
	{

	[CreateAssetMenu(menuName = "State Machine/Conditions/Is Character Controller on a Slope")]
	public class IsOnWalkableSlopeConditionSO : StateConditionSO<IsOnWalkableSlopeCondition> { }

	public class IsOnWalkableSlopeCondition : Condition
	{
		private PlayerController _playerController;

		public override void Awake(StateMachine stateMachine)
		{
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		protected override bool Statement() =>_playerController.CanWalkOnSlope && (_playerController.IsOnSlopeVertical || _playerController.SlopeInFront || _playerController.SlopeInBack) && !(_playerController.SlopeTag == "SpeedRamp" || _playerController.SlopeTag == "FinalRamp");
	}
}