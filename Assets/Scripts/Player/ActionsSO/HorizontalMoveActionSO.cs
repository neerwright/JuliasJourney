using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "HorizontalMove", menuName = "State Machine/Actions/Horizontal Move")]
	public class HorizontalMoveActionSO : StateActionSO<HorizontalMoveAction>
	{
		[Tooltip("Horizontal XZ plane speed multiplier")]
		public float speed = 8f;
	}

	public class HorizontalMoveAction : StateAction
	{
		//Component references
		private PlayerScript _player;
		private HorizontalMoveActionSO _originSO => (HorizontalMoveActionSO)base.OriginSO; // The SO this StateAction spawned from

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
		}

		public override void OnUpdate()
		{
			//delta.Time is used when the movement is applied (ApplyMovementVectorAction)
			_player.movementVector.x = _player.movementInput.x * _originSO.speed; 
		}
	}
}