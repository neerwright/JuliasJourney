using UnityEngine;
using Statemachine;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(fileName = "SetBoolVarAction", menuName = "State Machine/Actions/Set bool Var")]
	public class SetBoolVarActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        [SerializeField] public BoolVariableSO boolVar;
        [SerializeField] public bool value;

		protected override StateAction CreateAction() => new SetBoolVarAction();
	}

	public class SetBoolVarAction : StateAction
	{
		private BoolVariableSO _boolVar;
        private bool _value;
		private new SetBoolVarActionSO OriginSO => (SetBoolVarActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
            _boolVar = OriginSO.boolVar;
            _value = OriginSO.value;
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
				_boolVar.Value = _value;
		}


			public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
				_boolVar.Value = _value;
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
				_boolVar.Value = _value;
		}
	}
}