using System.Collections.Generic;
using UnityEngine;
using Utilities;
//using UnityEngine.Events;
using RewindSystem;
using Scriptables;

namespace Player
{

	public class PlayerRewind : MonoBehaviour 
	{

        [SerializeField] private GameObject _player;
        [SerializeField] private PlayerScript _playerScript;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private TimeController _timeController;
        [SerializeField] private GameObjectStringSet timeObjectsSet;

        private int index = 0;

        public void StartRewind(int maxIndex)
        {
            _playerController.IsRewinding = true;
            Debug.Log(maxIndex);
            index = maxIndex;
            
            
            
        }

        private void Awake()
        {
            Pair<GameObject, string> pair = new Pair<GameObject, string>();
            pair.ItemOne = this.transform.gameObject;
            pair.ItemTwo = "player";
            timeObjectsSet.Add(pair);
        }

        private void Update()
        {
            if(_playerScript.interactInput)
            {
                _timeController.Rewind = true;
                

            }

            if(_playerController.IsRewinding)
            {
                _player.transform.position = _timeController.getRecordedData("player", index).pos;
                index--;
                if ( index < 1)
                {
                    _playerController.IsRewinding = false;
                }
            }
        }
		
    }
}