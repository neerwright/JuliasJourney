using UnityEngine;
using Statemachine;
using Com.LuisPedroFonseca.ProCamera2D;

namespace Player
{

	[CreateAssetMenu(fileName = "ChangeCameraZoomInSettings", menuName = "State Machine/Actions/Change Camera ZoomIn Settings")]
	public class ChangeCameraZoomInSettingsActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        [SerializeField] private bool _disableWhenOneTarget = true;
		public bool DisableWhenOneTarget => _disableWhenOneTarget;

		protected override StateAction CreateAction() => new ChangeCameraZoomInSettings();
	}

	public class ChangeCameraZoomInSettings : StateAction
	{
		private PlayerScript _player;
        private ProCamera2DZoomToFitTargets _instance;
		private new ChangeCameraZoomInSettingsActionSO OriginSO => (ChangeCameraZoomInSettingsActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
            _instance = (ProCamera2DZoomToFitTargets) GameObject.FindObjectOfType(typeof(ProCamera2DZoomToFitTargets));
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
            {
                Debug.Log(_instance);
                if(_instance != null)
				    _instance.DisableWhenOneTarget = OriginSO.DisableWhenOneTarget;
            }
		}


			public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
            {
                Debug.Log(_instance);
                if(_instance != null)
				    _instance.DisableWhenOneTarget = OriginSO.DisableWhenOneTarget;
            }
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
            {
                Debug.Log(_instance);
                if(_instance != null)
				    _instance.DisableWhenOneTarget = OriginSO.DisableWhenOneTarget;
            }
            
		}
	}
}