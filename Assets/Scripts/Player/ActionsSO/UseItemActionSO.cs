using UnityEngine;
using Statemachine;
using VariableSO;

namespace Player
{

	[CreateAssetMenu(fileName = "UseItemAction", menuName = "State Machine/Actions/Use a usable Item")]
	public class UseItemActionSO : StateActionSO
    { 
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
			//if (_originSO.Moment == SpecificMoment.OnUpdate)
            //    pass;
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
			//if (_originSO.Moment == SpecificMoment.OnStateExit)
            //    pass;
		}
		
	}
}