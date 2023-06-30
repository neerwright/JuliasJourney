using UnityEngine;
using Statemachine;
using Animancer;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(fileName = "PlayRewindAnimationAction", menuName = "State Machine/Actions/Rewind Animation Clip")]
	public class RewindAnimationActionSO : StateActionSO
	{

        [SerializeField] private AnimationTrackerSO _animationData;
        public AnimationTrackerSO AnimationData => _animationData;


        
		protected override StateAction CreateAction() => new RewindAnimation();
	}

	public class RewindAnimation : StateAction
	{
        private PlayerScript _player;
        private PlayerAnimations pAnimns;

		private new RewindAnimationActionSO OriginSO => (RewindAnimationActionSO)base.OriginSO;
        private AnimancerComponent _animancer;
        private ClipTransition _clip;

        private AnimationTrackerSO _animationData;
        private AnimancerState _state;

		public override void Awake(StateMachine stateMachine)
		{
            _animationData = OriginSO.AnimationData;
			
            _player = stateMachine.GetComponent<PlayerScript>();
            pAnimns = _player.GetComponent<PlayerAnimations>();
            if(pAnimns)
            {
                _animancer = pAnimns.get_animancer(AnimancerObjects.Player);
            }
		}

		public override void OnUpdate()
		{
            var clip = _animationData.Clip;
            double time = _animationData.Time;
            if(clip != null)
                _animancer.Play(clip, 0.5f).Time = (float) time;
		}


			public override void OnStateEnter()
		{
		}

		public override void OnStateExit()
		{

		}
	}
}