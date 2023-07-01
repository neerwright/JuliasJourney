using UnityEngine;
using Statemachine;
using Sounds;

namespace Player
{

	[CreateAssetMenu(fileName = "PlaySFXAction", menuName = "State Machine/Actions/Play SFX")]
	public class PlaySFXActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        [SerializeField] private AudioClipGameEvent _audioClipGameEvent = default;
		public AudioClipGameEvent AudioClipGameEvent => _audioClipGameEvent;

        [SerializeField] private AudioClip _audioClip;
		public AudioClip AudioClip => _audioClip;

        [Range(0,1)]
        [SerializeField] float _volume;
		public float Volume => _volume;

        [SerializeField] private bool _playOnlyWhenLanding = false;
		public bool PlayOnlyWhenLanding => _playOnlyWhenLanding;

		protected override StateAction CreateAction() => new PlaySFX();
	}

	public class PlaySFX : StateAction
	{
		private new PlaySFXActionSO OriginSO => (PlaySFXActionSO)base.OriginSO;
        private PlayerController _playerController;

        public override void Awake(StateMachine stateMachine)
		{
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
            {
                if(!OriginSO.PlayOnlyWhenLanding || _playerController.LandingThisFrame)
                {
				    OriginSO.AudioClipGameEvent.Raise(OriginSO.AudioClip, OriginSO.Volume);
                }
                
            }
		}


		public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
            {
                if(!OriginSO.PlayOnlyWhenLanding || _playerController.LandingThisFrame)
                {
				    OriginSO.AudioClipGameEvent.Raise(OriginSO.AudioClip, OriginSO.Volume);
                }
                
            }
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
            {
                if(!OriginSO.PlayOnlyWhenLanding || _playerController.LandingThisFrame)
                {
				    OriginSO.AudioClipGameEvent.Raise(OriginSO.AudioClip, OriginSO.Volume);
                }
                
            }
            
		}
	}
}