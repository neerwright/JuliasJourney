using UnityEngine;
using Statemachine;
using Animancer;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(fileName = "PlayPlayJumpAnimation", menuName = "State Machine/Actions/Play Play Jump Animation")]
	public class PlayJumpAnimationActionSO : StateActionSO
	{

        [SerializeField] private AnimationTrackerSO _animationData;
        public AnimationTrackerSO AnimationData => _animationData;
        
        [SerializeField] private float _startClip = 0f;
		public float StartClip => _startClip;

        [SerializeField] private float _endClip =1f;
		public float EndClip => _endClip;

        [SerializeField] private ClipTransition _clip;
        public ClipTransition Clip => _clip;
        
		protected override StateAction CreateAction() => new PlayJumpAnimation();
	}

	public class PlayJumpAnimation: StateAction
	{
        private PlayerScript _player;
        private PlayerAnimations pAnimns;

		private new PlayJumpAnimationActionSO OriginSO => (PlayJumpAnimationActionSO)base.OriginSO;
        private AnimancerComponent _animancer;
        private ClipTransition _clip;

        private AnimationTrackerSO _animationData;
        private AnimancerState _state;

        private float _startClip = 0f;
        private float _endClip = 1f;
        private float _currTime;
		public override void Awake(StateMachine stateMachine)
		{
            _startClip = OriginSO.StartClip;
            _endClip = OriginSO.EndClip;

            _animationData = OriginSO.AnimationData;
			//_animancer = OriginSO.Animancer;
            _clip = OriginSO.Clip;
            _player = stateMachine.GetComponent<PlayerScript>();
            pAnimns = _player.GetComponent<PlayerAnimations>();
            if(pAnimns)
            {
                _animancer = pAnimns.get_animancer();
            }

            _currTime = _startClip;
		}

		public override void OnUpdate()
		{
            _animationData.Clip = _clip.Clip;
            
            _animationData.Time = _currTime;			
            
            if(_currTime > _endClip)
                 return;

            if(_clip != null)
                _animancer.Play(_clip, AnimancerPlayable.DefaultFadeDuration).Time = _currTime ;

            _currTime += Time.deltaTime;
            
		}


			public override void OnStateEnter()
		{
			
		}

		public override void OnStateExit()
		{
		}
	}
}