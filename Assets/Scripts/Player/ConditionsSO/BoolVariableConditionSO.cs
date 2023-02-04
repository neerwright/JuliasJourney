using UnityEngine;
using Statemachine;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(menuName = "State Machine/Conditions/VariableSO is true Condition")]
	public class BoolVariableConditionSO : StateConditionSO<BoolVariableCondition> 
    { 
		[TextArea] public string description;

        public BoolVariableSO BoolVariable;
    }

	public class BoolVariableCondition : Condition
	{
		//Component references
		private BoolVariableConditionSO _originSO => (BoolVariableConditionSO)base.OriginSO; // The SO this Condition spawned from

		public override void Awake(StateMachine stateMachine)
		{
		}

		protected override bool Statement() => _originSO.BoolVariable.Value;
	}
}