using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace environment
{
    public class GlideTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameStateSO _gameState;
        [SerializeField]
        private bool _startGlide;
        

        private void OnTriggerEnter2D(Collider2D Collider)
        {
            if(Collider.gameObject.tag == "Player")
            {
                if(_startGlide)
                    _gameState.UpdateGameState(GameState.Glide);
                else
                    _gameState.UpdateGameState(GameState.Gameplay);
            
            }
        }
    }
}