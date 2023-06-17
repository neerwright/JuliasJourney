using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "SlopeSlideDown", menuName = "State Machine/Actions/Slope Slide down")]
	public class SlopeSlideDownActionSO : StateActionSO<SlopeSlideDownAction>
	{
		[Tooltip("Speed multiplyer")]
		public float speed = 8f;
		
	}

	public class SlopeSlideDownAction : StateAction
	{
		//Component references
		private PlayerScript _player;
		private PlayerController _playerController;
		private SlopeSlideDownActionSO _originSO => (SlopeSlideDownActionSO)base.OriginSO; // The SO this StateAction spawned from
		
		private bool _hitSlope = false;
		private Vector2 SlopeVector;

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		public override void OnUpdate()
		{			
			float xMove = SlopeVector.x;
			float yMove = SlopeVector.y;
			float ratio = xMove / yMove;
			
			_player.movementVector.x += xMove * _originSO.speed * Time.deltaTime ;
			_player.movementVector.y += yMove * _originSO.speed * Time.deltaTime ;

			// move closer to edge until we touch it
			if(!_hitSlope) 
			{
				if(!_playerController.IsGrounded)
				{
					_player.movementVector.y -= 10 * Time.deltaTime;
				}
				else
				{
					_hitSlope = true;
				}
			}
				

			if(_player.movementVector.y < PlayerScript.MAX_FALL_SPEED)
			{
				_player.movementVector.y = PlayerScript.MAX_FALL_SPEED;
				_player.movementVector.x = _player.movementVector.y * ratio;
			}	

						
		}

		public override void OnStateEnter()
		{
			SlopeVector = _playerController.VectorAlongSlope;
			if(_player.transform.localScale.x == -1)
			{
				SlopeVector = -SlopeVector;
			}

			// keep velocity but smopther movement along slope
			if(_player.movementVector.y > 0) 
			{
				_player.movementVector = -SlopeVector * _player.movementVector.magnitude;
			}	
		}

		public override void OnStateExit()
		{
			_hitSlope = false;	
		}
		
	}
}