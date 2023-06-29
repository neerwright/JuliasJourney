using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace environment
{
   public class Fence : MonoBehaviour , IEnvironmentalObject
    {
        [SerializeField]
        private float speedThreashold;

        [SerializeField]
        private GameObject _player;

        private PlayerScript _playerScript; 
        public void Initialize(GameObject player)
        {
            _player = player;
            _playerScript = _player.GetComponent<PlayerScript>();
        }

        private void OnTriggerEnter2D(Collider2D Collider)
        {
            if(Collider.gameObject.tag == "Player")
            {
                if( _playerScript.movementVector.x > speedThreashold)
                {
                    Destroy(gameObject);
                }
            }
        }
    } 
}

