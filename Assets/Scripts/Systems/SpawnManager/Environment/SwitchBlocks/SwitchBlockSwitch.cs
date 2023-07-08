using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
namespace environment 
{
    
    public class SwitchBlockSwitch : MonoBehaviour
    {
        [SerializeField] private GameEvent _switchEvent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] BlockType _type;

        private bool _triggered = false;
        private IEnumerator  _coroutine;

        private const string PLAYER_TAG = "Player";
        private const string PUSHBOX_TAG = "PushBox";
        private const float DELAY = 0.05f;

        void Start()
        {
            _spriteRenderer.sprite = _sprites[0];
            _coroutine = PlayAnimation();
        }
        
        void OnTriggerEnter2D(Collider2D collider)
        {
            if(_triggered)
                return;

            if(collider.tag == PLAYER_TAG || collider.tag == PUSHBOX_TAG)
            {
                StartCoroutine(_coroutine);
                _switchEvent.Raise();
                _triggered = true;
            }
            
        }

       public  void OnReset(Collider2D collider)
        {
            StopCoroutine(_coroutine);
            _triggered = false;

            _spriteRenderer.sprite = _sprites[0];
            
        }

        private IEnumerator PlayAnimation()
        {
            _spriteRenderer.sprite = _sprites[1];
            yield return new WaitForSeconds(DELAY);
            _spriteRenderer.sprite = _sprites[2];
        }
    }
}

