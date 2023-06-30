using UnityEngine;
using Statemachine;
using Animancer;
using Scriptables;

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

        [SerializeField] private AnimationTrackerSO _animationData;
        public AnimationTrackerSO AnimationData => _animationData;
        
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

        private AnimationTrackerSO _animationData;
        private AnimancerState _state;
        private AnimationClip _currClip;

		public override void Awake(StateMachine stateMachine)
		{
            _animationData = OriginSO.AnimationData;
            _jumpLandingClip = OriginSO.JumpClip;
            _runningClip = OriginSO.RunClip;
            _playerController = stateMachine.GetComponent<PlayerController>();
            _player = stateMachine.GetComponent<PlayerScript>();
            pAnimns = _player.GetComponent<PlayerAnimations>();
            
            if(pAnimns)
            {
                _animancer = pAnimns.get_animancer(AnimancerObjects.Player);
            }
		}

		public override void OnUpdate()
		{
			_animationData.Clip = _currClip;
            if(_state != null)
                _animationData.Time = _state.Time;
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
            _currClip = _runningClip.Clip;
            if(!_animancer)
            {
                _animancer = pAnimns.get_animancer(AnimancerObjects.Player);
            }

            if(_animancer)
            {
                _state = _animancer.Play(_runningClip);
            }
        }

        private void PlayLandingAnim()
        {
            _currClip = _jumpLandingClip.Clip;
            if(!_animancer)
                {
                    _animancer = pAnimns.get_animancer(AnimancerObjects.Player);
                }

            if(_animancer)
            {
                _state = _animancer.Play(_jumpLandingClip);
                _state.Events.OnEnd = PlayRunningAnim;
            }
        }
	}
}