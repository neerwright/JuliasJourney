using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "SlopeMovement", menuName = "State Machine/Actions/testtesttest")]
	public class InvertedRampActionSO : StateActionSO<InvertedRampAction>
	{
		[Tooltip("Speed multiplyer")]
		public float gravityOnRamp = 8f;

		[Tooltip("Speed multiplyer")]
		public float speedUp = 16f;

		[Tooltip("Speed multiplyer")]
		public float speedDown = 16f;

		[Range(1f,10f)]
		[Tooltip("Speed multiplyer")]
		public float initialSpeedBoost = 1f;
		
	}

	public class InvertedRampAction : StateAction
	{
		//Component references
		private PlayerScript _player;
		private PlayerController _playerController;
		private InvertedRampActionSO _originSO => (InvertedRampActionSO)base.OriginSO; // The SO this StateAction spawned from
		
		private Vector2 _slopeVector;

		
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
			//, move up slope even when standing still
			if ( _playerController.IsCompletelyOnSlope)
			{
				_player.movementVector.x += _slopeVector.x * _originSO.gravityOnRamp * Time.deltaTime;
				_player.movementVector.y += _slopeVector.y * _originSO.gravityOnRamp* Time.deltaTime;
			}

			//slide down: 
			if(Mathf.Sign(inputSign) != Mathf.Sign(_slopeVector.y))
			{               
				//linear
                _player.movementVector.x = _slopeVector.x * input * _originSO.speedDown ;
				_player.movementVector.y = (_slopeVector.y * input * _originSO.speedDown) ;
									
			}
			else
			{	
                //accellerate			
                _player.movementVector.x += _slopeVector.x * input * Time.deltaTime * _originSO.speedUp;
				_player.movementVector.y += _slopeVector.y * input  * Time.deltaTime * _originSO.speedUp;
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