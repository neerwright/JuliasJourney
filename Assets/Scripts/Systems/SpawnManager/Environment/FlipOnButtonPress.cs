using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
using Player;

namespace environment 
{
    public class FlipOnButtonPress : MonoBehaviour, IEnvironmentalObject
    {

            
        [SerializeField] private GameEvent _playerFlip;

        private GameObject _player;
        private PlayerScript _playerScript; 

        public void Initialize(GameObject player)
        {
            _player = player;
            _playerScript = _player.GetComponent<PlayerScript>();
        }

        void OnTriggerStay2D(Collider2D collider)
        {
            if(_playerScript.jumpInput)
            {
                _playerFlip.Raise();
            }
        }
    }
}

