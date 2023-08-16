using UnityEngine;
using Statemachine;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(fileName = "EnableDisable", menuName = "State Machine/Actions/Enable Disable GO with Tag")]
	public class EnableDisableObjectsWithTagActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        public GameObjectVariableSO objectToEnable;
		public GameObjectVariableSO objectToDisable;


		protected override StateAction CreateAction() => new nableDisableObjects();
	}

	public class nableDisableObjects : StateAction
	{

		private new EnableDisableObjectsWithTagActionSO OriginSO => (EnableDisableObjectsWithTagActionSO)base.OriginSO;

		
		private GameObject _objectToEnable;
		public GameObject _objectToDisable;

		public override void Awake(StateMachine stateMachine)
		{
			_objectToEnable = OriginSO.objectToEnable.GObject;
			_objectToDisable = OriginSO.objectToDisable.GObject;
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
			{

			}
		}


		public override void OnStateEnter()
		{
			_objectToEnable = OriginSO.objectToEnable.GObject;
			_objectToDisable = OriginSO.objectToDisable.GObject;
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
			{
				if(_objectToEnable)
				{
					_objectToEnable.SetActive(true);
				}
				if(_objectToEnable)
				{
					_objectToDisable.SetActive(false);
				}
			}
				
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
			{
				if(_objectToEnable)
				{
					_objectToEnable.SetActive(true);
				}
				if(_objectToEnable)
				{
					_objectToDisable.SetActive(false);
				}
			}
		}
	}
}