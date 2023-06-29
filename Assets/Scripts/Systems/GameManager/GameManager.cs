using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStateSO _gameState = default;


        private void Start()
        {
            StartGame();
        }

        void StartGame()
        {
            _gameState.UpdateGameState(GameState.Gameplay);
        }
    }
}

