using UnityEngine;
using Statemachine;

namespace Player
{

	[CreateAssetMenu(fileName = "HorizontalBoost", menuName = "State Machine/Actions/Horizontal Boost")]
	public class HorizontalBoostActionSO : StateActionSO<HorizontalBoostAction>
	{
		[Tooltip("Horizontal XZ plane speed multiplier")]
		public float boostSpeed = 2f;
	}

	public class HorizontalBoostAction : StateAction
	{
		//Component references
		private Player _player;
		private HorizontalBoostActionSO _originSO => (HorizontalBoostActionSO)base.OriginSO; // The SO this StateAction spawned from

		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<Player>();
		}

		public override void OnUpdate()
		{
            float input = _player.movementInput.x;
			//delta.Time is used when the movement is applied (ApplyMovementVectorAction)
			_player.movementVector.x += _originSO.boostSpeed * Time.deltaTime * input; 
		}
	}
}