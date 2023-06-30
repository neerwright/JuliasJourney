using UnityEngine;
using Statemachine;
using GameManager;

namespace Player
{

	[CreateAssetMenu(menuName = "State Machine/Conditions/GameSTate Condition")]
	public class GameStateConditionSO : StateConditionSO<GameStateCondition> 
    { 
		[TextArea] public string description;

        public GameStateSO gameState;
        public GameState gameStateCondition;
    }

	public class GameStateCondition : Condition
	{
		//Component references
		private GameStateConditionSO _originSO => (GameStateConditionSO)base.OriginSO; // The SO this Condition spawned from
        private GameStateSO _gameState;
        private GameState _gameStateCondition;

		public override void Awake(StateMachine stateMachine)
		{
            _gameState = _originSO.gameState;
            _gameStateCondition = _originSO.gameStateCondition;
		}

		protected override bool Statement() => (_gameState.CurrentGameState == _gameStateCondition);
	}
}