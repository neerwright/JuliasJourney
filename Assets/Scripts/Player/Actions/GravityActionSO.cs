using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "ApplyGravityAction", menuName = "State Machine/Actions/Apply Gravity")]
	public class GravityActionSO : StateActionSO<GravityAction> 
	{ 
		[Tooltip("Vertical movement pulling down the player to keep it anchored to the ground.")]
		public float verticalPull = -5f;
	}

	public class GravityAction : StateAction
	{
		//Component references
		private Player _player;
		private PlayerController _playerController;
		private new GravityActionSO _originSO => (GravityActionSO)base.OriginSO; // The SO this StateAction spawned from


		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<Player>();
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		public override void OnUpdate()
		{
			_player.movementVector.y = _originSO.verticalPull;
		}
	}
}