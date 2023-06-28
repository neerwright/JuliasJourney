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

        [SerializeField] private AnimationClip _clip;
        public AnimationClip Clip => _clip;
        

        [SerializeField] private float _speed = 1f;
        public float Speed => _speed;

        [SerializeField] private float _transition = 0.25f;
        public float Transition => _transition;

		protected override StateAction CreateAction() => new PlayJumpAnimation();
	}

	public class PlayJumpAnimation: StateAction
	{
        private PlayerScript _player;
        private PlayerAnimations pAnimns;

		private new PlayJumpAnimationActionSO OriginSO => (PlayJumpAnimationActionSO)base.OriginSO;
        private AnimancerComponent _animancer;
        private AnimationClip _clip;

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

            _currTime = OriginSO.StartClip;
		}

		public override void OnUpdate()
		{
            _animationData.Clip = _clip;
            
            _animationData.Time = _currTime;			
            
            if(_currTime > OriginSO.EndClip)
            {
                Debug.Log("end");
                _currTime = OriginSO.EndClip;
            }
                 

            if(_clip != null)
            {
                Debug.Log(_currTime);
                _animancer.Play(_clip, OriginSO.Transition).Time = _currTime ;
            }
                

            _currTime += OriginSO.Speed * Time.deltaTime;
            
		}


		public override void OnStateEnter()
		{
			_currTime = OriginSO.StartClip;
		}

		public override void OnStateExit()
		{
		}
	}
}