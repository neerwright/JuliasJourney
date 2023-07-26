using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace environment
{
    public class SwitchBlockTrigger : MonoBehaviour , IEnvironmentalObject
    {
        
        [SerializeField] private GameEvent _switchBlocksEvent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _lookDirectionSprites;
        [SerializeField] private Sprite[] _closeEyeSprites;

        private const string PLAYER_TAG = "Player";
        private const string PUSHBOX_TAG = "PushBox";
        private const float DELAY = 0.05f;
        private const float BIG_DELAY = 0.1f;
        private const float LOOK_VERTICAL_OFFSET = 3f;
        private const float LOOK_UP_OFFSET = 1f;

        private const float INFLUENCE_RADIUS = 60f;

        private GameObject _player;
        private bool _triggered = false;

        private Vector2 _pos;
        



        public void Initialize(GameObject player)
        {
            _player = player;
        }

        public void Reseting()
        {
            _spriteRenderer.sprite = _closeEyeSprites[0];
            _triggered = false;
        }


        void Start()
        {
            _spriteRenderer.sprite = _closeEyeSprites[0];
            _pos = transform.position;
        }
        
        void Update()
        {
            if(_triggered)
                    return;

            PlayLookAnimation();
        }
        void OnTriggerEnter2D(Collider2D collider)
        {
            if(_triggered)
                    return;
                    
            if(Vector2.Distance(_player.transform.position , gameObject.transform.position) >  INFLUENCE_RADIUS)
                return;
            
            if(collider.tag == PLAYER_TAG || collider.tag == PUSHBOX_TAG)
            {
                _triggered = true;
                _switchBlocksEvent?.Raise();
                StartCoroutine(PlayEyeAnimation());
            }
        }

        private void PlayLookAnimation()
        {
            if(_player == null)
                return;

            Vector2 playerPosition = _player.transform.position;
            //look up
            if( playerPosition.y >  _pos.y + LOOK_UP_OFFSET)
            {
                
                if(playerPosition.x < _pos.x - LOOK_VERTICAL_OFFSET)
                {
                    //look left
                     _spriteRenderer.sprite = _lookDirectionSprites[0];
                }
                else if(playerPosition.x >  _pos.x + LOOK_VERTICAL_OFFSET)
                {
                    //look right
                     _spriteRenderer.sprite = _lookDirectionSprites[2];
                }
                else
                {
                    //look up
                     _spriteRenderer.sprite = _lookDirectionSprites[1];
                }
            }
            else
            {
                //look sideways
                if(playerPosition.x < _pos.x - LOOK_VERTICAL_OFFSET)
                {
                    //look left
                     _spriteRenderer.sprite = _lookDirectionSprites[3];
                }
                else if(playerPosition.x >  _pos.x + LOOK_VERTICAL_OFFSET)
                {
                    //look right
                     _spriteRenderer.sprite = _lookDirectionSprites[5];
                }
                else
                {
                    //look up
                     _spriteRenderer.sprite = _lookDirectionSprites[4];
                }
            }
        }

        private IEnumerator PlayEyeAnimation()
        {
            int size = _closeEyeSprites.Length;
            for (int i = 0; i < size; i++) 
            {
                _spriteRenderer.sprite = _closeEyeSprites[i];
                yield return new WaitForSeconds(DELAY);
                
            }
            yield return new WaitForSeconds(BIG_DELAY);


            for(int i = _closeEyeSprites.Length -1; i > 0 ; i--)
            {
                _spriteRenderer.sprite = _closeEyeSprites[i];
                yield return new WaitForSeconds(DELAY);
                
            }
            yield return new WaitForSeconds(BIG_DELAY);
            
            
            
            _triggered = false;
        }
    }

}

