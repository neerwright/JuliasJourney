using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "ResetCoyote", menuName = "State Machine/Actions/Reset Coyote jump parameter")]
	public class ResetCoyoteJumpActionSO : StateActionSO<ResetCoyoteJumpAction> 
	{ 
	}

	public class ResetCoyoteJumpAction : StateAction
	{
		//Component references
		private PlayerController _playerController;


		public override void Awake(StateMachine stateMachine)
		{
			_playerController = stateMachine.GetComponent<PlayerController>();
		}


		public override void OnStateEnter()
		{
			_playerController.CoyoteUsable = false;
			_playerController.TimeLeftGrounded = float.MinValue;	
		}

		public override void OnUpdate()
		{

		}
	}
}