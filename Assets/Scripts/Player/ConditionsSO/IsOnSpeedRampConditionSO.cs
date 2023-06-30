using UnityEngine;
using Statemachine;

namespace Player
	{

	[CreateAssetMenu(menuName = "State Machine/Conditions/Is Character On Speed Ramp?")]
	public class IsOnSpeedRampConditionSO : StateConditionSO<IsOnSpeedRampCondition> 
	{ 
		public bool finalRamp = false;
	}

	public class IsOnSpeedRampCondition : Condition
	{
		private PlayerController _playerController;
		private IsOnSpeedRampConditionSO _originSO => (IsOnSpeedRampConditionSO)base.OriginSO;
		private string _tag;
		private bool _finalRamp = false;

		public override void Awake(StateMachine stateMachine)
		{
			_finalRamp = _originSO.finalRamp;
			_playerController = stateMachine.GetComponent<PlayerController>();
			_tag = "SpeedRamp";
			if(_finalRamp)
				_tag = "FinalRamp";
		}

		protected override bool Statement() => (_playerController.SlopeInFront || _playerController.IsOnSlopeVertical) && _playerController.SlopeTag == _tag;
	}
}