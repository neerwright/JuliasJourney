using UnityEngine;
using Statemachine;
using Animancer;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(fileName = "PlayAnimationAction", menuName = "State Machine/Actions/Play Animation Clip")]
	public class PlayAnimationActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        [SerializeField] private AnimationTrackerSO _animationData;
        public AnimationTrackerSO AnimationData => _animationData;
        //[SerializeField] private AnimancerComponent _animancer;
        //public AnimancerComponent Animancer => _animancer;

        [SerializeField] private ClipTransition _clip;
        public ClipTransition Clip => _clip;

        public AnimancerObjects animancerObject = AnimancerObjects.Player;
        public bool loop = true;
        
		protected override StateAction CreateAction() => new PlayAnimation();
	}

	public class PlayAnimation : StateAction
	{
        private PlayerScript _player;
        private PlayerAnimations pAnimns;

		private new PlayAnimationActionSO OriginSO => (PlayAnimationActionSO)base.OriginSO;
        private AnimancerComponent _animancer;
        private ClipTransition _clip;

        private AnimationTrackerSO _animationData;
        private AnimancerState _state;

		public override void Awake(StateMachine stateMachine)
		{
            _animationData = OriginSO.AnimationData;
			//_animancer = OriginSO.Animancer;
            _clip = OriginSO.Clip;
            _player = stateMachine.GetComponent<PlayerScript>();
            pAnimns = _player.GetComponent<PlayerAnimations>();
            if(pAnimns)
            {   
                    _animancer = pAnimns.get_animancer(OriginSO.animancerObject);         
            }
		}

		public override void OnUpdate()
		{
            if(_animationData != null)
            {
                _animationData.Clip = _clip.Clip;

                if(_state != null)
                _animationData.Time = _state.Time;
            }           
			
            if (OriginSO.Moment == SpecificMoment.OnUpdate)
            {
                _state = _animancer.Play(_clip);
            }
		}


		public override void OnStateEnter()
		{
            if(!OriginSO.loop)
            {
                if(_state != null)
                {
                    _state.Time = 0;
                }
            }
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
			{
                if(!_animancer)
                {
                     _animancer = pAnimns.get_animancer(OriginSO.animancerObject);
                }

                if(_animancer)
                {
                    _state = _animancer.Play(_clip);
                }
            }
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
			{
                _state = _animancer.Play(_clip);
            }
		}
	}
}