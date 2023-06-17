using UnityEngine;
using Statemachine;

namespace Player
{
	[CreateAssetMenu(menuName = "State Machine/Conditions/Has pressed interact")]
	public class IsPressingInteractConditionSO : StateConditionSO<IsPressingInteractCondition> { }

	public class IsPressingInteractCondition : Condition
	{
		//Component references
		private PlayerScript _player;
		public override void Awake(StateMachine stateMachine)
		{
			_player = stateMachine.GetComponent<PlayerScript>();
		}

		protected override bool Statement() => _player.interactInput;
	}
}