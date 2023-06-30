using System.Collections.Generic;
using UnityEngine;
using Utilities;
//using UnityEngine.Events;
using RewindSystem;
using Scriptables;
using GameManager;

namespace Player
{

	public class Poketwatch : MonoBehaviour 
	{

        
        [SerializeField] private PlayerScript _playerScript;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] GameEvent startRewinding;
        [SerializeField] GameEvent stopRewinding;
        [SerializeField] GameStateSO _gameState;

        private void StartRewind()
        {
            if(_gameState.CurrentGameState ==  GameState.Gameplay)
            {
                startRewinding?.Raise();
                _playerController.IsRewinding = true;
            }
            
        }
        

        public void StopRewind()
        {
            stopRewinding?.Raise();
            _playerController.IsRewinding = false;
        }
		
    }
}