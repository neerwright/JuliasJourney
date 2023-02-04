using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "StopMovementAction", menuName = "State Machine/Actions/Stop Movement")]
	public class StopMovementActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

		protected override StateAction CreateAction() => new StopMovement();
	}

	public class StopMovement : StateAction
	{
		private Player _player;
		private new StopMovementActionSO OriginSO => (StopMovementActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<Player>();
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
				_player.movementVector = Vector2.zero;
		}


			public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
				_player.movementVector = Vector2.zero;
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
				_player.movementVector = Vector2.zero;
		}
	}
}