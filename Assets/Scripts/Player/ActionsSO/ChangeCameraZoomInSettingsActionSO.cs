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

		[SerializeField] private float _delay = 2f;
		public float Delay => _delay;

		protected override StateAction CreateAction() => new ChangeCameraZoomInSettings();
	}

	public class ChangeCameraZoomInSettings : StateAction
	{
		private PlayerScript _player;
        private ProCamera2DZoomToFitTargets _instance;
		private new ChangeCameraZoomInSettingsActionSO OriginSO => (ChangeCameraZoomInSettingsActionSO)base.OriginSO;

		private float _time = 0f;
		private bool _startDelay = false;

		public override void Awake(StateMachine stateMachine)
		{
			 _time = 0f;
			_player = stateMachine.GetComponent<PlayerScript>();
            _instance = (ProCamera2DZoomToFitTargets) GameObject.FindObjectOfType(typeof(ProCamera2DZoomToFitTargets));
		}

		public override void OnUpdate()
		{

			if(_startDelay)
			{
				_time += Time.deltaTime;
				if(_time > OriginSO.Delay)
				{
					if(_instance != null)
					{
						_instance.DisableWhenOneTarget = OriginSO.DisableWhenOneTarget;
						Debug.Log("Disable");
					}
				    	

					_startDelay = false;
				}
			}
		}


		public override void OnStateEnter()
		{
			
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
            {
				_time = 0f;
                _startDelay = true;
				if(_instance == null)
					_instance = (ProCamera2DZoomToFitTargets) GameObject.FindObjectOfType(typeof(ProCamera2DZoomToFitTargets));
					Debug.Log(_instance);
            }
		}

		public override void OnStateExit()
		{
			
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
            {
				if(_instance == null)
					_instance = (ProCamera2DZoomToFitTargets) GameObject.FindObjectOfType(typeof(ProCamera2DZoomToFitTargets));

				if(_instance != null)
					_instance.DisableWhenOneTarget = OriginSO.DisableWhenOneTarget;
            }
			
            
		}
	}
}