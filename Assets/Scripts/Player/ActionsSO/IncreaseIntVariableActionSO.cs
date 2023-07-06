using UnityEngine;
using Statemachine;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(fileName = "IncreaseIntVariable", menuName = "State Machine/Actions/Increase IntVariable")]
	public class IncreaseIntVariableActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        [Tooltip("if set to false, will increase the variable with the int value")]
        [SerializeField] public bool setVaribale = true;
        [SerializeField] public IntVariableSO intVar;
        [SerializeField] public int value;

		protected override StateAction CreateAction() => new IncreaseIntVariable();
	}

	public class IncreaseIntVariable : StateAction
	{
		private IntVariableSO _intVar;
        private int _value;

		private new IncreaseIntVariableActionSO OriginSO => (IncreaseIntVariableActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
            _intVar = OriginSO.intVar;
            _value = OriginSO.value;
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
            {
                if(OriginSO.setVaribale)
                {
                    _intVar.Value = _value;  
                }
                else
                {
                    _intVar.Value += _value;
                }
            }
				
		}


		public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
			{
                if(OriginSO.setVaribale)
                {
                    _intVar.Value = _value;  
                }
                else
                {
                    _intVar.Value += _value;
                }
            }
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
			{
                if(OriginSO.setVaribale)
                {
                    _intVar.Value = _value;  
                }
                else
                {
                    _intVar.Value += _value;
                }
            }
		}
	}
}