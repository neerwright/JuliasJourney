using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace GameManager 
{
    public enum GameState
    {
        Gameplay, //regular state: player moves, attacks, can perform actions
        Pause, //pause menu is opened, the whole game world is frozen
        Reset, //when inventory UI or cooking UI are open
        Cutscene,
        Glide
    }

    [CreateAssetMenu(fileName = "GameState", menuName = "Gameplay/GameState")]
    public class GameStateSO : ScriptableObject
    {
        public GameState CurrentGameState => _currentGameState;
        
        [Header("Game states")]
        [SerializeField] private GameState _currentGameState = default;
        [SerializeField] private GameState _previousGameState = default;
        [SerializeField] private GameEvent _onReset;
        

        public void UpdateGameState(GameState newGameState)
        {
            if (newGameState == CurrentGameState)
                return;

            if (newGameState == GameState.Reset)
            {
                _onReset?.Raise();
            }

            _previousGameState = _currentGameState;
            _currentGameState = newGameState;
        }

        public void ResetToPreviousGameState()
        {
            if (_previousGameState == _currentGameState)
                return;

            
            GameState stateToReturnTo = _previousGameState;
            _previousGameState = _currentGameState;
            _currentGameState = stateToReturnTo;
        }
    }
}

