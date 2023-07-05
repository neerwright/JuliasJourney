using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "WingAnimationAction", menuName = "State Machine/Actions/Play Wing Animation")]
	public class PlayWingAnimationActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        [SerializeField] private WingAnimationState _wingAnimationToPlay = default;
		public WingAnimationState WingAnimationToPlay => _wingAnimationToPlay;

		protected override StateAction CreateAction() => new PlayWingAnimation();
	}

	public class PlayWingAnimation : StateAction
	{
		private WingsAnimator _wingsAnimator;
		private new PlayWingAnimationActionSO OriginSO => (PlayWingAnimationActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
			_wingsAnimator = stateMachine.GetComponentInChildren<WingsAnimator>();
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
			{
                PlayAnimation();
            }
		}


			public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
				PlayAnimation();
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
				PlayAnimation();
		}

        public void PlayAnimation()
        {
            switch(OriginSO.WingAnimationToPlay) 
                {
                case WingAnimationState.Idle:
                    _wingsAnimator.PlayIdle();
                    break;
                case WingAnimationState.Up:
                    _wingsAnimator.PlayUp();
                    break;
                case WingAnimationState.Down:
                    _wingsAnimator.PlayDown();
                    break;
                case WingAnimationState.Glide:
                    _wingsAnimator.PlayGlide();
                    break;
                case WingAnimationState.Inactive:
                    _wingsAnimator.Deactivate();
                    break;
                case WingAnimationState.Run:
                    _wingsAnimator.PlayRun();
                    break;
                case WingAnimationState.Boost:
                    _wingsAnimator.PlayBoost();
                    break;
                default:
                    _wingsAnimator.PlayIdle();
                    break;
                }
        }
	}
}