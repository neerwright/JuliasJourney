using UnityEngine;
using Statemachine;
using System.Collections;

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

        private bool _changedBounds = false;
        private const float DISTANCE = 4f;

        private Transform _player;



		public override void Awake(StateMachine stateMachine)
		{
			_playerController = stateMachine.GetComponent<PlayerController>();
            _player = _playerController.gameObject.transform;
		}

		public override void OnUpdate()
		{
			if (OriginSO.Moment == SpecificMoment.OnUpdate)
            {

                RaycastHit2D hit = Physics2D.Raycast(_player.position, Vector2.down, DISTANCE, LayerMask.GetMask("Ground"));
                
                if(OriginSO.ResetBounds)
                {
                    if (hit.collider != null && !_changedBounds)
                    {
                        _changedBounds = true;
                        _playerController.ResetBounds();
                    }
                }
                else
                {
                    if (hit.collider != null  && !_changedBounds)
                    {
                        _playerController.CharacterBounds = OriginSO.CharacterBounds;
                        _changedBounds = true;
                    }
                }
            }
            
				
		}


		public override void OnStateEnter()
		{
            _changedBounds = false;
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