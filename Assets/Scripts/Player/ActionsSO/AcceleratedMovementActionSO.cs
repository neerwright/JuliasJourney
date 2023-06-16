using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "HorizontalAcceleratedMovement", menuName = "State Machine/Actions/Horizontal accelerated Movement")]
	public class AcceleratedMovementActionSO : StateActionSO<AcceleratedMovementAction>
	{
		[Tooltip("slower Maximum speed, only when accelerati g on ground")]
		public float maxSpeedOnGround = 9f;

		[Tooltip("Maximum speed, if faster, slowly damp towards it")]
		public float absoluteMaxSpeed = 30f;

		[Tooltip("Horizontal acceleration")]
		public float acceleration = 3f;

		[Tooltip("time it takes for the acceleration to get dampened")]
		[SerializeField]
		[Range(0,1)]
		public float damping = 0f;
	}

	public class AcceleratedMovementAction : StateAction
	{
		//Component references
		private Player _player;
		private PlayerController _playerController;
		private AcceleratedMovementActionSO _originSO => (AcceleratedMovementActionSO)base.OriginSO; // The SO this StateAction spawned from

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<Player>();
			_playerController = stateMachine.GetComponent<PlayerController>();

		}

		public override void OnUpdate()
		{
			_player.movementVector.y = 0f;

			if (Mathf.Abs(_player.movementVector.x) <= _originSO.maxSpeedOnGround)
			{
				//acceleration --> delta.Time is used 
				_player.movementVector.x += _player.movementInput.x  * _originSO.acceleration * Time.deltaTime;
				//MaxSpeed
				_player.movementVector.x = Mathf.Clamp(_player.movementVector.x, -_originSO.maxSpeedOnGround, _originSO.maxSpeedOnGround);
			}
			else
			{
				//Absolute MaxSpeed
				_player.movementVector.x = Mathf.Clamp(_player.movementVector.x, -_originSO.absoluteMaxSpeed, _originSO.absoluteMaxSpeed);
			}
			
			//Decelerate
			_player.movementVector.x *= Mathf.Pow(1f - _originSO.damping, Time.deltaTime * 10f);
			
			//collision
			if (_playerController.IsCollidingWithWall)
				_player.movementVector.x = 0;
			
			
			

		}
	}
}