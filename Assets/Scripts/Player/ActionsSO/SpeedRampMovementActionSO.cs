using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "SpeedRampMovement", menuName = "State Machine/Actions/SpeedRamp")]
	public class SpeedRampMovementActionSO : StateActionSO<SpeedRampMovementAction>
	{
		[Tooltip("Speed multiplyer")]
		public float speed = 1f;

		[Tooltip("Speed multiplyer")]
		public float acceleration = 50f;
		
	}

	public class SpeedRampMovementAction : StateAction
	{
		//Component references
		private PlayerScript _player;
		private PlayerController _playerController;
		private SpeedRampMovementActionSO _originSO => (SpeedRampMovementActionSO)base.OriginSO; // The SO this StateAction spawned from
		
		private bool _useSlopeMovement = false;
		private float _angleCorrection;

		private const float TRANSITION_ALONG_SLOPE_MULT = -1.0f;
		
		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		public override void OnUpdate()
		{
			_useSlopeMovement = false;
			_angleCorrection = 0;
			CheckMovementAndCalculateAngleCorrection();

			if(_useSlopeMovement)
			{
				Vector2 SlopeVector = _playerController.VectorAlongSlope;
				Debug.DrawRay(_player.transform.position, SlopeVector, Color.red);
				
				float movementSign = Mathf.Sign(_player.movementVector.x);

				if(Mathf.Sign(movementSign) != Mathf.Sign(SlopeVector.y))
				{
					
					//slide up: accelerate
					_player.movementVector.x += SlopeVector.x * -_player.movementInput.x * _originSO.acceleration * Time.deltaTime;
					_player.movementVector.y += (SlopeVector.y * -_player.movementInput.x * _originSO.acceleration + _angleCorrection * 10f) * Time.deltaTime;
				}
				else
				{
					//sudden turnaround from walking up  to down
					if(Mathf.Sign(_player.movementVector.x) != Mathf.Sign(_player.movementInput.x))
					{
						
						_player.movementVector.x = SlopeVector.x * movementSign* _originSO.speed  ;
						_player.movementVector.y = (SlopeVector.y * movementSign * _originSO.speed + _angleCorrection ) ;
					}
					else
					{
						//walk doen ramp: linear movement
						_player.movementVector.x = SlopeVector.x * -_player.movementInput.x * _originSO.speed ;
						_player.movementVector.y = (SlopeVector.y * -_player.movementInput.x * _originSO.speed + _angleCorrection) ;
					}	
					

					
				}
					
			}
			else
			{
				_player.movementVector.y = 0;
				//_player.movementVector.x = _player.movementInput.x * _originSO.speed;
			}
		

			
		}

		public override void OnStateEnter()
		{
			
			_playerController.TouchedSlope = true;
			Vector2 SlopeVector = _playerController.VectorAlongSlope;
			if(_player.transform.localScale.x == 1)
			{
				SlopeVector = -SlopeVector;
			}
			_player.movementVector = SlopeVector * _player.movementVector.magnitude;
				
		}

		private void CheckMovementAndCalculateAngleCorrection()
		{
			if (_playerController.IsOnSlopeVertical && _playerController.SlopeInBack)
			{
				_useSlopeMovement = true;	
			}
			

			//smooth out transition when in front of a slope and starting to walk up
			if (_playerController.SlopeInFront && !_playerController.IsOnSlopeVertical)
			{
				_useSlopeMovement = true;
				
				_angleCorrection = Vector2.Dot(_playerController.VectorAlongSlope, Vector2.up) * TRANSITION_ALONG_SLOPE_MULT;				
			}
			
			if (_playerController.SlopeInFront && _playerController.IsOnSlopeVertical)
				_angleCorrection = 0.1f;
			{
				_useSlopeMovement = true;
			}
			
			//smooth out transition when we start walking down a slope
			if(_playerController.IsCompletelyOnSlope && !_playerController.SlopeInFront && !_playerController.SlopeInBack)
			{
				_useSlopeMovement = true;
				_angleCorrection = 10f;								
			}


		}
	}
}