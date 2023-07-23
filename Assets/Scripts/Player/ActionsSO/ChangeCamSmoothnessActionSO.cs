using UnityEngine;
using Statemachine;
using Com.LuisPedroFonseca.ProCamera2D;

namespace Player
{

	[CreateAssetMenu(fileName = "ChangeCamSmoothness", menuName = "State Machine/Actions/Change Cam Smoothness")]
	public class ChangeCamSmoothnessActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        [SerializeField] private float _smoothness = 1f;
		public float Smoothness => _smoothness;

		protected override StateAction CreateAction() => new ChangeCamSmoothness();
	}

	public class ChangeCamSmoothness : StateAction
	{
        //private ProCamera2DZoomToFitTargets _instance;
		private new ChangeCamSmoothnessActionSO OriginSO => (ChangeCamSmoothnessActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
            //_instance = (ProCamera2DZoomToFitTargets) GameObject.FindObjectOfType(typeof(ProCamera2DZoomToFitTargets));
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
            {
                ProCamera2D.Instance.VerticalFollowSmoothness = OriginSO.Smoothness;
            }
		}


			public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
            {
                ProCamera2D.Instance.VerticalFollowSmoothness = OriginSO.Smoothness;
            }
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
            {
                ProCamera2D.Instance.VerticalFollowSmoothness = OriginSO.Smoothness;
            }
            
		}
	}
}