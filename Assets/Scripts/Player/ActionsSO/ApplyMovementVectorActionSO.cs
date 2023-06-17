using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "ApplyMovementVector", menuName = "State Machine/Actions/Apply Movement Vector")]
	public class ApplyMovementVectorActionSO : StateActionSO<ApplyMovementVectorAction> { }

	public class ApplyMovementVectorAction : StateAction
	{
		//Component references
		private PlayerScript _player;
		private PlayerController _playerController;

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		public override void OnUpdate()
		{

		}

		public override void OnFixedUpdate()
		{
			_playerController.Move(_player.movementVector * Time.deltaTime);//applies the current movementVector
		}
	}
}