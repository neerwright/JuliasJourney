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
		
	}

	public class SlopeMovementAction : StateAction
	{
		//Component references
		private PlayerScript _player;
		private PlayerController _playerController;
		private SlopeMovementActionSO _originSO => (SlopeMovementActionSO)base.OriginSO; // The SO this StateAction spawned from
		
		private Vector2 _slopeVector;
		private bool _useSlopeMovement = false;
		private float _angleCorrection = 0f;

		private const float TRANSITION_ALONG_SLOPE_MULT = 5.0f;
		
		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		public override void OnUpdate()
		{			
			Debug.DrawRay(_player.transform.position, _slopeVector * 10f, Color.red);

			float input = _player.movementInput.x;	
			float inputSign = Mathf.Sign(input);
			//gravity, move down slope even when standing still
			if ( input == 0)
			{
				_player.movementVector = new Vector2(0,0);
				
				//_player.movementVector.x -= _slopeVector.x * -_originSO.gravityOnRamp * Time.deltaTime;
				//_player.movementVector.y -= _slopeVector.y * -_originSO.gravityOnRamp* Time.deltaTime;
				return;
			}

			//slide down: 
			if(Mathf.Sign(inputSign) != Mathf.Sign(_slopeVector.y))
			{
				if(!_playerController.SlopeInBack && !_playerController.IsCompletelyOnSlope)
				{
					//we are starting to walk down a slope -> smooth transition onto slope
					_player.movementVector.x += input * Time.deltaTime * _originSO.speedDown;
					return;
				}
				else if (!_playerController.SlopeInBack && _playerController.IsCompletelyOnSlope)
				{
					//moment we have both feet under ledge -> walk down 
					_player.movementVector = _slopeVector * _player.movementVector.magnitude;
				}
				
				Debug.Log("acell");
				//accelerate
				_player.movementVector.x += _slopeVector.x * input * Time.deltaTime * _originSO.speedDown;
				_player.movementVector.y += _slopeVector.y * input  * Time.deltaTime * _originSO.speedDown;
				
					
			}
			else
			{
				//walk up slope: linear movement
				Debug.Log("lin");
				_player.movementVector.x = _slopeVector.x * input * _originSO.speedUp ;
				_player.movementVector.y = (_slopeVector.y * input * _originSO.speedUp) ;
				
				//walked completey up -> smooth transition to walking
				if(!_playerController.SlopeInFront && !_playerController.IsCompletelyOnSlope)
				{
					Debug.Log("S");
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
			
			_slopeVector = _playerController.VectorAlongSlope;
			
			_slopeVector = -_slopeVector;
			
			_player.movementVector = _slopeVector * _player.movementVector.magnitude;
			
				
		}

		private void AngleCorrection()
		{
			_angleCorrection = 0f;
			if (_playerController.IsOnSlopeVertical && _playerController.SlopeInBack)
			{
			}
			

			//smooth out transition when in front of a slope and starting to walk up
			if (_playerController.SlopeInFront && !_playerController.IsOnSlopeVertical)
			{
				//if (Mathf.Abs(_player.movementInput.x) > Mathf.Epsilon)
					//_angleCorrection = -10f ;//Vector2.Dot(_playerController.VectorAlongSlope, Vector2.up) * TRANSITION_ALONG_SLOPE_MULT;
					
				//if(_angleCorrection > 0) // stay more grounded until slope is under our feet
				//	_angleCorrection = -_angleCorrection;
				
			}
			
			if (_playerController.SlopeInFront && _playerController.IsOnSlopeVertical)
			{
			}
			
			//smooth out transition when we start walking down a slope
			if(_playerController.IsCompletelyOnSlope && !_playerController.SlopeInFront && !_playerController.SlopeInBack)
			{

				//if (Mathf.Abs(_player.movementInput.x) > Mathf.Epsilon)
				//	_angleCorrection = Vector2.Dot(_playerController.VectorAlongSlope, Vector2.up);
				
				//if(_angleCorrection > 0) // stay closer to slope 
				//	_angleCorrection = -_angleCorrection;	

			}


		}
	}
}