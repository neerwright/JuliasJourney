using UnityEngine;
using Statemachine;
using Animancer;

namespace Player
{

	[CreateAssetMenu(fileName = "PlayAnimationAction", menuName = "State Machine/Actions/Play Animation Clip")]
	public class PlayAnimationActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        //[SerializeField] private AnimancerComponent _animancer;
        //public AnimancerComponent Animancer => _animancer;

        [SerializeField] private ClipTransition _clip;
        public ClipTransition Clip => _clip;
        
		protected override StateAction CreateAction() => new PlayAnimation();
	}

	public class PlayAnimation : StateAction
	{
        private PlayerScript _player;
        private PlayerAnimations pAnimns;

		private new PlayAnimationActionSO OriginSO => (PlayAnimationActionSO)base.OriginSO;
        private AnimancerComponent _animancer;
        private ClipTransition _clip;

		public override void Awake(StateMachine stateMachine)
		{
			//_animancer = OriginSO.Animancer;
            _clip = OriginSO.Clip;
            _player = stateMachine.GetComponent<PlayerScript>();
            pAnimns = _player.GetComponent<PlayerAnimations>();
            if(pAnimns)
            {
                _animancer = pAnimns.get_animancer();
            }
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
            {
                _animancer.Play(_clip);
            }
		}


			public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
			{
                if(!_animancer)
                {
                    _animancer = pAnimns.get_animancer();
                }

                if(_animancer)
                {
                    _animancer.Play(_clip);
                }
            }
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
			{
                _animancer.Play(_clip);
            }
		}
	}
}