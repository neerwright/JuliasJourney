using UnityEngine;
using Statemachine;
using Sounds;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(fileName = "PlaySeriesOfSFXsoundsActionSO", menuName = "State Machine/Actions/Play Series Of SFX sounds ")]
	public class PlaySeriesOfSFXsoundsActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

		[SerializeField] private IntVariableSO _index;
		public IntVariableSO IndexSO => _index;

        [SerializeField] private AudioClipGameEvent _audioClipGameEvent = default;
		public AudioClipGameEvent AudioClipGameEvent => _audioClipGameEvent;

        [SerializeField] private AudioClip[] _audioClips;
		public AudioClip[] AudioClips => _audioClips;

        [Range(0,1)]
        [SerializeField] float[] _volumes;
		public float[] Volumes => _volumes;

		protected override StateAction CreateAction() => new PlaySeriesOfSFXsounds();
	}

	public class PlaySeriesOfSFXsounds : StateAction
	{
		private new PlaySeriesOfSFXsoundsActionSO OriginSO => (PlaySeriesOfSFXsoundsActionSO)base.OriginSO;
        private PlayerController _playerController;

        public override void Awake(StateMachine stateMachine)
		{
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
            {
				int index = OriginSO.IndexSO.Value;
				if (index >= OriginSO.AudioClips.Length)
					index = OriginSO.AudioClips.Length - 1;

				OriginSO.AudioClipGameEvent.Raise(OriginSO.AudioClips[index], OriginSO.Volumes[index]);
                
            }
		}


		public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
            {
				int index = OriginSO.IndexSO.Value;
				if (index >= OriginSO.AudioClips.Length)
					index = OriginSO.AudioClips.Length - 1;

				OriginSO.AudioClipGameEvent.Raise(OriginSO.AudioClips[index], OriginSO.Volumes[index]);
                
            }
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
            {
				int index = OriginSO.IndexSO.Value;
				if (index >= OriginSO.AudioClips.Length)
					index = OriginSO.AudioClips.Length - 1;

				OriginSO.AudioClipGameEvent.Raise(OriginSO.AudioClips[index], OriginSO.Volumes[index]);
                
            }
            
		}
	}
}