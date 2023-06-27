using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "ChangeCharacterBoundsAction", menuName = "State Machine/Actions/Change Character Bounds")]
	public class ChangeCharacterBoundsActionSO : StateActionSO
	{
		[SerializeField] private StateAction.SpecificMoment _moment = default;
		public StateAction.SpecificMoment Moment => _moment;

        [SerializeField] private Bounds _characterBounds;
        public Bounds CharacterBounds => _characterBounds;

        [SerializeField] private bool _resetBounds = false;
		public bool ResetBounds => _resetBounds;

		protected override StateAction CreateAction() => new ChangeCharacterBounds();
	}

	public class ChangeCharacterBounds : StateAction
	{
		private PlayerController _playerController;
		private new ChangeCharacterBoundsActionSO OriginSO => (ChangeCharacterBoundsActionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
			_playerController = stateMachine.GetComponent<PlayerController>();
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
            {
                if(OriginSO.ResetBounds)
                {
                    _playerController.ResetBounds();
                }
                else
                {
                    _playerController.CharacterBounds = OriginSO.CharacterBounds;
                }
            }
				
		}


			public override void OnStateEnter()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateEnter)
			{
                if(OriginSO.ResetBounds)
                {
                    _playerController.ResetBounds();
                }
                else
                {
                    _playerController.CharacterBounds = OriginSO.CharacterBounds;
                }
            }
		}

		public override void OnStateExit()
		{
			if (OriginSO.Moment == SpecificMoment.OnStateExit)
			{
                if(OriginSO.ResetBounds)
                {
                    _playerController.ResetBounds();
                }
                else
                {
                    _playerController.CharacterBounds = OriginSO.CharacterBounds;
                }
            }
		}
	}
}