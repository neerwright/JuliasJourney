using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManagement;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStateSO _gameState = default;

        [SerializeField] private LoadEventSO _nextIslandEvent;
        [SerializeField] private GameSceneSO _nextIsland;
        private bool _startedGame = false;

        private void Start()
        {
            //StartGame();
        }

        public void StartGame()
        {
            if(!_startedGame)
            {   
                Application.targetFrameRate = 60;
                _gameState.UpdateGameState(GameState.Cutscene);
                //load level 2, where the object for the cutscene gets spawned
                _nextIslandEvent?.Raise(_nextIsland);
                _startedGame = true;
            }
            
            
        }
    }
}

