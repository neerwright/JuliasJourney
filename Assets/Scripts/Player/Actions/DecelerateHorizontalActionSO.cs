using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "DecelerateHorizontalAction", menuName = "State Machine/Actions/DecelerateHorizontal")]
	public class DecelerateHorizontalActionSO : StateActionSO
	{
		public float _deAcceleration = 1f;

		protected override StateAction CreateAction() => new DecelerateHorizontalAction();
	}

	public class DecelerateHorizontalAction : StateAction
	{
		private Player _player;
		private new DecelerateHorizontalActionSO OriginSO => (DecelerateHorizontalActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<Player>();
		}

		public override void OnUpdate()
		{
			_player.movementVector.y = 0;
			_player.movementVector.x = Mathf.MoveTowards(_player.movementVector.x, 0, OriginSO._deAcceleration * Time.deltaTime);
		}


			public override void OnStateEnter()
		{

		}

		public override void OnStateExit()
		{

		}
	}
}