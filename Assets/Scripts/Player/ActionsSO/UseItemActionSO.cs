using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "UseItemAction", menuName = "State Machine/Actions/Use a usable Item")]
	public class UseItemActionSO : StateActionSO<UseItemAction> { }

	public class UseItemAction : StateAction
	{
		//Component references
		private Player _player;
		private PlayerController _playerController;

		public override void Awake(StateMachine stateMachine)
		{

		}

		public override void OnStateEnter()
		{

		}

        public override void OnUpdate()
		{
		}
		
	}
}