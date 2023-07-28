using System.Collections.Generic;
using UnityEngine;
using Utilities;
//using UnityEngine.Events;
using RewindSystem;
using Scriptables;
using GameManager;
using Com.LuisPedroFonseca.ProCamera2D;

namespace Player
{

	public class Poketwatch : MonoBehaviour 
	{

        
        [SerializeField] private PlayerScript _playerScript;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] GameEvent startRewinding;
        [SerializeField] GameEvent stopRewinding;
        [SerializeField] GameStateSO _gameState;

        [SerializeField] private Transform _secondaryPlayerTransform;

        float _originalFixedDeltaTime;
        void Start()
        {
            _originalFixedDeltaTime = Time.fixedDeltaTime;
        }
        private void StartRewind()
        {
            Debug.Log("Start Rewind");
            if(_gameState.CurrentGameState ==  GameState.Gameplay)
            {
                Debug.Log("Start Rewind Gameplay");
                Time.fixedDeltaTime = _originalFixedDeltaTime;
                Time.timeScale = 1f;
                //ProCamera2D.Instance.AddCameraTarget(_secondaryPlayerTransform,0.2f, 0.2f, 0.3f);
                startRewinding?.Raise();
                _playerController.IsRewinding = true;
            }
            
        }
        

        public void StopRewind()
        {
            //ProCamera2D.Instance.RemoveCameraTarget(_secondaryPlayerTransform);
            stopRewinding?.Raise();
            _playerController.IsRewinding = false;
        }
		
    }
}