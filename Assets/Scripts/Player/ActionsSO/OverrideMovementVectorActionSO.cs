using UnityEngine;
using Statemachine;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(fileName = "OverrideMovementVectorAction", menuName = "State Machine/Actions/Override MovementVector")]
	public class OverrideMovementVectorActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        [SerializeField] private Vector2VariableSO _varSO ;
		public Vector2VariableSO VarSO => _varSO;

		protected override StateAction CreateAction() => new OverrideMovementVector();
	}

	public class OverrideMovementVector : StateAction
	{
		private PlayerScript _player;
		private new OverrideMovementVectorActionSO OriginSO => (OverrideMovementVectorActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
				_player.movementVector = OriginSO.VarSO.Value;
		}


			public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
				_player.movementVector = OriginSO.VarSO.Value;
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
				_player.movementVector = OriginSO.VarSO.Value;
		}
	}
}