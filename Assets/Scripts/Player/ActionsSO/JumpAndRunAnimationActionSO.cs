using UnityEngine;
using Statemachine;
using Animancer;

namespace Player
{

	[CreateAssetMenu(fileName = "PlayJumpAndRunAnimationAction", menuName = "State Machine/Actions/Play Animation Clip for running and jumping")]
	public class JumpAndRunAnimationActionSO : StateActionSO
	{
		//[SerializeField] private StateAction.SpecificMoment _moment = default;
		//public StateAction.SpecificMoment Moment => _moment;


        [SerializeField] private ClipTransition _jumpLandingClip;
        public ClipTransition JumpClip => _jumpLandingClip;

        [SerializeField] private ClipTransition _RunningClip;
        public ClipTransition RunClip => _RunningClip;
        
		protected override StateAction CreateAction() => new PlayJumpAndRunAnimation();
	}

	public class PlayJumpAndRunAnimation : StateAction
	{
        private PlayerScript _player;
        private PlayerController _playerController;
        private PlayerAnimations pAnimns;

		private new JumpAndRunAnimationActionSO OriginSO => (JumpAndRunAnimationActionSO)base.OriginSO;
        private AnimancerComponent _animancer;
        private ClipTransition _jumpLandingClip;
        private ClipTransition _runningClip;

		public override void Awake(StateMachine stateMachine)
		{
            _jumpLandingClip = OriginSO.JumpClip;
            _runningClip = OriginSO.RunClip;
            _playerController = stateMachine.GetComponent<PlayerController>();
            _player = stateMachine.GetComponent<PlayerScript>();
            pAnimns = _player.GetComponent<PlayerAnimations>();
            
            if(pAnimns)
            {
                _animancer = pAnimns.get_animancer();
            }
		}

		public override void OnUpdate()
		{
			
		}


			public override void OnStateEnter()
		{
                
                if(_playerController.LandingThisFrame)
                {
                    PlayLandingAnim();
                }
                else
                {
                    PlayRunningAnim();
                }
                
                
                
            
		}

		public override void OnStateExit()
		{
			
		}

        public void PlayRunningAnim()
        {
            if(!_animancer)
            {
                _animancer = pAnimns.get_animancer();
            }

            if(_animancer)
            {
                _animancer.Play(_runningClip);
            }
        }

        private void PlayLandingAnim()
        {
            
            if(!_animancer)
                {
                    _animancer = pAnimns.get_animancer();
                }

            if(_animancer)
            {
                var state = _animancer.Play(_jumpLandingClip);
                state.Events.OnEnd = PlayRunningAnim;
            }
        }
	}
}