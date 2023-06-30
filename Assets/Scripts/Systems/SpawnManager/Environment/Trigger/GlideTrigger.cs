using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace environment
{
    public class GlideTrigger : MonoBehaviour, IEnvironmentalObject
    {
        [SerializeField]
        private GameStateSO _gameState;
        
        public void Initialize(GameObject player)
        {

        }

        private void OnTriggerEnter2D(Collider2D Collider)
        {
            Debug.Log("trig");
            if(Collider.gameObject.tag == "Player")
            {
                Debug.Log("trigsdsd");
                _gameState.UpdateGameState(GameState.Glide);
            }
        }
    }
}