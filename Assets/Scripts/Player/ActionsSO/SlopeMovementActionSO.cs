using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "SlopeMovement", menuName = "State Machine/Actions/Slope Movement")]
	public class SlopeMovementActionSO : StateActionSO<SlopeMovementAction>
	{
		[Tooltip("Speed multiplyer")]
		public float gravityOnRamp = 8f;

		[Tooltip("Speed multiplyer")]
		public float speedUp = 16f;

		[Tooltip("Speed multiplyer")]
		public float speedDown = 16f;

		[Tooltip("Speed multiplyer")]
		public float turnSpeed = 1f;

		[Range(1f,10f)]
		[Tooltip("Speed multiplyer")]
		public float initialSpeedBoost = 1f;
		
	}

	public class SlopeMovementAction : StateAction
	{
		//Component references
		private PlayerScript _player;
		private PlayerController _playerController;
		private SlopeMovementActionSO _originSO => (SlopeMovementActionSO)base.OriginSO; // The SO this StateAction spawned from
		
		private Vector2 _slopeVector;

		private const float CLOSER_TO_LEDGE_MULT = 5.0f;
		private const float PUSH_PLAYER_CLOSER_TO_LEDGE_THRESHOLD = 10f;
		
		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		public override void OnUpdate()
		{	
			_slopeVector = -1 * _playerController.VectorAlongSlope;		
			Debug.DrawRay(_player.transform.position, _slopeVector * 10f, Color.red);

			float input = _player.movementInput.x;	
			float inputSign = Mathf.Sign(input);
			//gravity, move down slope even when standing still
			if ( input == 0 && _playerController.IsCompletelyOnSlope)
			{
				
				_player.movementVector.x -= _slopeVector.x * -_originSO.gravityOnRamp * Time.deltaTime;
				_player.movementVector.y -= _slopeVector.y * -_originSO.gravityOnRamp* Time.deltaTime;
				return;
			}

			//slide down: 
			if(Mathf.Sign(inputSign) != Mathf.Sign(_slopeVector.y))
			{
				if(!_playerController.SlopeInBack && !_playerController.IsCompletelyOnSlope && _playerController.IsGrounded)
				{
					//we are starting to walk down a slope -> smooth transition onto slope
					_player.movementVector.x += input * Time.deltaTime * _originSO.speedDown;
					if (input == 0)
						_player.movementVector = new Vector2 (0f,0f);
					return;
				}
				else if (!_playerController.SlopeInBack && _playerController.IsCompletelyOnSlope)
				{
					//moment we have both feet under ledge -> walk down 
					_player.movementVector = _slopeVector * _player.movementVector.magnitude;
					if(Mathf.Abs(_player.movementVector.x) > PUSH_PLAYER_CLOSER_TO_LEDGE_THRESHOLD)
					{
						_player.movementVector.y -= CLOSER_TO_LEDGE_MULT * Time.deltaTime;
					}
						
				}
				
				//accelerate
				_player.movementVector.x += _slopeVector.x * input * Time.deltaTime * _originSO.speedDown;
				_player.movementVector.y += _slopeVector.y * input  * Time.deltaTime * _originSO.speedDown;
				
					
			}
			else
			{
				//walk up slope: linear movement
				_player.movementVector.x = _slopeVector.x * input * _originSO.speedUp ;
				_player.movementVector.y = (_slopeVector.y * input * _originSO.speedUp) ;
				
				//walked completey up -> smooth transition to walking
				if(!_playerController.SlopeInFront && !_playerController.IsCompletelyOnSlope)
				{
					_player.movementVector.x = input * _originSO.speedUp;
					if (_player.movementVector.y > 0f)
						_player.movementVector.y = 0f;

					//touch ground
					_player.movementVector.y -= _originSO.speedUp;	
					return;
				}

				//sudden turnaround from walking up  to down
				if(Mathf.Sign(_player.movementVector.x) != inputSign)
				{
					_player.movementVector.x = _slopeVector.x * input * _originSO.turnSpeed  ;
					_player.movementVector.y = (_slopeVector.y * input  * _originSO.turnSpeed ) ;
				}	
			}
						
		}

		public override void OnStateEnter()
		{
			_playerController.TouchedSlope = true;
			
			_slopeVector = -1 * _playerController.VectorAlongSlope;
						
			_player.movementVector = _slopeVector * _player.movementVector.magnitude * _originSO.initialSpeedBoost;
			
				
		}


		
	}
}