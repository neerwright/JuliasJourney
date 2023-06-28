using System.Collections.Generic;
using UnityEngine;
using Utilities;
//using UnityEngine.Events;
using RewindSystem;
using Scriptables;

namespace Player
{

	public class Poketwatch : MonoBehaviour 
	{

        
        [SerializeField] private PlayerScript _playerScript;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] GameEvent startRewinding;
        [SerializeField] GameEvent stopRewinding;

        private void StartRewind()
        {
            startRewinding?.Raise();
            _playerController.IsRewinding = true;
        }
        

        public void StopRewind()
        {
            stopRewinding?.Raise();
            _playerController.IsRewinding = false;
        }
		
    }
}