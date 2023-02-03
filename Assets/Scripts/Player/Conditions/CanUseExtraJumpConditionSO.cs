using UnityEngine;
using Statemachine;
using VariableSO;

namespace Player
{

	[CreateAssetMenu(menuName = "State Machine/Conditions/Can use Coyote Jump")]
	public class CanUseExtraJumpConditionSO : StateConditionSO<CanUseExtraJumpCondition> 
    { 
        public BoolVariableSO _isInsideCollider;
    }

	public class CanUseExtraJumpCondition : Condition
	{
		//Component references
		private CanUseExtraJumpConditionSO _originSO => (CanUseExtraJumpConditionSO)base.OriginSO; // The SO this Condition spawned from

		public override void Awake(StateMachine stateMachine)
		{
		}

		protected override bool Statement() => _originSO._isInsideCollider.Value;
	}
}