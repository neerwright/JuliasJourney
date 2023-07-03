using UnityEngine;
using Statemachine;
using System.Linq;

namespace Player
{

	[CreateAssetMenu(fileName = "PlayParticlesAction", menuName = "State Machine/Actions/Play Particlest")]
	public class PlayParticlesActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        [SerializeField] private string _particleSystemTag;
		public string ParticleSystemTag => _particleSystemTag;

		protected override StateAction CreateAction() => new PlayParticles();
	}

	public class PlayParticles : StateAction
	{
		private ParticleSystem _playerPS;
		private new PlayParticlesActionSO OriginSO => (PlayParticlesActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
			ParticleSystem[] PS = stateMachine.GetComponentsInChildren<ParticleSystem>();
            _playerPS = (ParticleSystem) PS.Where(x => x.gameObject.tag == OriginSO.ParticleSystemTag).FirstOrDefault();
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
				_playerPS.Play();
		}


		public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
				_playerPS.Play();
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
				_playerPS.Play();
		}
	}
}