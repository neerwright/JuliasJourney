using UnityEngine;
using Statemachine;
using Scriptables;
using System.Linq;

namespace Player
{

	[CreateAssetMenu(fileName = "UseItemAction", menuName = "State Machine/Actions/Use a usable Item")]
	public class UseItemActionSO : StateActionSO
    { 
        [Tooltip("GameObject inside SO has to be a usable item, else nothing happens")]
        [SerializeField] private GameObjectSet _itemsToUse;
		public GameObjectSet ItemsToUse => _itemsToUse;

        [SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        protected override StateAction CreateAction() => new UseItemAction();
    }

	public class UseItemAction : StateAction
	{
        private Player _player;
        private UseItemActionSO _originSO => (UseItemActionSO)base.OriginSO; // The SO this StateAction spawned from

        public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<Player>();
		}

		public override void OnUpdate()
		{
			if (_originSO.Moment == SpecificMoment.OnUpdate)
            {
                GameObject targetItem = _originSO.ItemsToUse.Items.OrderBy(p => Vector2.Distance(p.transform.position, _player.transform.position)).FirstOrDefault();
                IUsableItem UsableItem = targetItem.GetComponent<IUsableItem>();
                UsableItem?.Use();
            }  
		}


		public override void OnStateEnter()
		{
			if (_originSO.Moment == SpecificMoment.OnStateEnter)
            {
                GameObject targetItem = _originSO.ItemsToUse.Items.OrderBy(p => Vector2.Distance(p.transform.position, _player.transform.position)).FirstOrDefault();
                IUsableItem UsableItem = targetItem.GetComponent<IUsableItem>();
                UsableItem?.Use();
            }    
		}

		public override void OnStateExit()
		{
			if (_originSO.Moment == SpecificMoment.OnStateExit)
            {
                GameObject targetItem = _originSO.ItemsToUse.Items.OrderBy(p => Vector2.Distance(p.transform.position, _player.transform.position)).FirstOrDefault();
                IUsableItem UsableItem = targetItem.GetComponent<IUsableItem>();
                UsableItem?.Use();
            }  
		}
		
	}
}