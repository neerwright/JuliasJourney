using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Player;
using GameManager;
using Com.LuisPedroFonseca.ProCamera2D;

namespace environment 
{
    public class SplashAnimationTrigger : MonoBehaviour
    {
        //SpriteRenderer _spriterRenderer
        //AnimationClip _Clip;
        [SerializeField]private AnimancerComponent _animancer;
        [SerializeField] private ClipTransition _clip;

        [SerializeField] private GameStateSO _playerState;
        private Vector2 _startingPos;

        private const float OFFSET = 0f;

        void Awake()
        {
            _startingPos = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.tag == "Player")
            {
                Vector2 pos = collider.gameObject.transform.position;
                pos.y += OFFSET;
                gameObject.transform.position = pos; 
                _animancer.Play(_clip);
                _playerState.UpdateGameState(GameState.Water);

                Transform model =  collider.gameObject.transform.Find("PlayerSkate"); 
                if(model != null)
                    model.gameObject.SetActive(false);
            }

            
        }

        public void ResetPosition()
        {
            transform.position = _startingPos;
        }


    }
}

