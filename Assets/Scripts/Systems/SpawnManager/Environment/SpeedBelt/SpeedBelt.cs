using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace environment
{
    public class SpeedBelt : MonoBehaviour, IEnvironmentalObject
    {
        [SerializeField]
        private float speedBoost;

        [SerializeField]
        private bool speedRight;

        private GameObject _player;

        private PlayerScript _playerScript; 

        public void Initialize(GameObject player)
        {
            _player = player;
            _playerScript = _player.GetComponent<PlayerScript>();
        }


        public void SpeedUpPlayer()
        {
               
                if(_playerScript != null)
                {
                    float dir = speedRight? 1 : -1;    
                    _playerScript.movementVector.x += dir * Time.deltaTime * speedBoost;
                }
                
        }
        


    }
}