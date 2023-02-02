using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "SlopeMovement", menuName = "State Machine/Actions/Slope Movement")]
	public class SlopeMovementActionSO : StateActionSO<SlopeMovementAction>
	{
		[Tooltip("Speed multiplyer")]
		public float speed = 8f;
		
	}

	public class SlopeMovementAction : StateAction
	{
		//Component references
		private Player _player;
		private PlayerController _playerController;
		private SlopeMovementActionSO _originSO => (SlopeMovementActionSO)base.OriginSO; // The SO this StateAction spawned from
		
		private bool _useSlopeMovement = false;
		private float _angleCorrection;

		private const float TRANSITION_ALONG_SLOPE_MULT = 5.0f;
		
		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<Player>();
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		public override void OnUpdate()
		{
			_useSlopeMovement = false;
			_angleCorrection = 0;
			CheckMovementAndCalculateAngle();

			if(_useSlopeMovement)
			{
				Vector2 SlopeVector = _playerController.VectorAlongSlope;
				Debug.DrawRay(_player.transform.position, SlopeVector, Color.red);
				
				_player.movementVector.x = SlopeVector.x * -_player.movementInput.x * _originSO.speed;
				_player.movementVector.y = SlopeVector.y * -_player.movementInput.x * _originSO.speed + _angleCorrection;		
			}
			else
			{
				_player.movementVector.y = 0;
				_player.movementVector.x = _player.movementInput.x * _originSO.speed;
			}
		

			
		}
		private void CheckMovementAndCalculateAngle()
		{
			if (_playerController.IsOnSlopeVertical && _playerController.SlopeInBack)
			{
				_useSlopeMovement = true;	
			}
			

			//smooth out transition when in front of a slope and starting to walk up
			if (_playerController.SlopeInFront && !_playerController.IsOnSlopeVertical)
			{
				_useSlopeMovement = true;
				if (Mathf.Abs(_player.movementInput.x) > Mathf.Epsilon)
					_angleCorrection = Vector2.Dot(_playerController.VectorAlongSlope, Vector2.up) * TRANSITION_ALONG_SLOPE_MULT;
					
				if(_angleCorrection > 0) // stay more grounded until slope is under our feet
					_angleCorrection = -_angleCorrection;
				
			}
			
			if (_playerController.SlopeInFront && _playerController.IsOnSlopeVertical)
			{
				_useSlopeMovement = true;
			}
			
			//smooth out transition when we start walking down a slope
			if(_playerController.IsCompletelyOnSlope && !_playerController.SlopeInFront && !_playerController.SlopeInBack)
			{
				_useSlopeMovement = true;

				if (Mathf.Abs(_player.movementInput.x) > Mathf.Epsilon)
					_angleCorrection = Vector2.Dot(_playerController.VectorAlongSlope, Vector2.up);
				
				if(_angleCorrection > 0) // stay closer to slope 
					_angleCorrection = -_angleCorrection;	

			}


		}
	}
}