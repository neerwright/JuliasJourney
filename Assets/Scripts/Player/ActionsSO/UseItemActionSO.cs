using UnityEngine;
using Statemachine;
using Scriptables;

namespace Player
{

	[CreateAssetMenu(fileName = "UseItemAction", menuName = "State Machine/Actions/Use a usable Item")]
	public class UseItemActionSO : StateActionSO
    { 
        [Tooltip("GameObject inside SO has to be a usable item, else nothing happens")]
        [SerializeField] private GameObjectVariableSO _itemToUse;
		public GameObjectVariableSO ItemToUse => _itemToUse;

        [SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        protected override StateAction CreateAction() => new UseItemAction();
    }

	public class UseItemAction : StateAction
	{
        private UseItemActionSO _originSO => (UseItemActionSO)base.OriginSO; // The SO this StateAction spawned from

		public override void OnUpdate()
		{
			if (_originSO.Moment == SpecificMoment.OnUpdate)
            {
                IUsableItem UsableItem = _originSO.ItemToUse.GameObject.GetComponent<IUsableItem>();
                UsableItem?.Use();
            }  
		}


		public override void OnStateEnter()
		{
			if (_originSO.Moment == SpecificMoment.OnStateEnter)
            {
                IUsableItem UsableItem = _originSO.ItemToUse.GameObject.GetComponent<IUsableItem>();
                UsableItem?.Use();
            }    
		}

		public override void OnStateExit()
		{
			if (_originSO.Moment == SpecificMoment.OnStateExit)
            {
                IUsableItem UsableItem = _originSO.ItemToUse.GameObject.GetComponent<IUsableItem>();
                UsableItem?.Use();
            }  
		}
		
	}
}