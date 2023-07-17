using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "SetRotationAction", menuName = "State Machine/Actions/SetRotation")]
	public class SetRotationActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        public string modelToRotateTag;
        public Vector3 rotation;


		protected override StateAction CreateAction() => new SetRotation();
	}

	public class SetRotation : StateAction
	{
		private GameObject _toRotate;
		private new SetRotationActionSO OriginSO => (SetRotationActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
			_toRotate = GameObject.FindWithTag(OriginSO.modelToRotateTag);
		}

		public override void OnUpdate()
		{
			//if (OriginSO.Moment == SpecificMoment.OnUpdate)
				//_toRotate.transform.rotation = Quaternion.Euler(OriginSO.rotation);
		}


		public override void OnStateEnter()
		{
			//if (OriginSO.Moment == SpecificMoment.OnStateEnter)
				//_toRotate.transform.rotation = Quaternion.Euler(OriginSO.rotation);
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
				_toRotate.transform.rotation = Quaternion.Euler(OriginSO.rotation);
		}

	}
}