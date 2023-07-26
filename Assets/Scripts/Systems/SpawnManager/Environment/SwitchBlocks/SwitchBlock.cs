using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace environment
{
    public enum BlockType
    {
        blue,
        red
    }
    public class SwitchBlock : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;
        [SerializeField] private Collider2D _collider;
        [SerializeField] BlockType _type;
        
        private bool active = false;
        public bool _rewinding = false;
        
        public void Switch()
        {
            if(active)
            {
                _spriteRenderer.sprite = _inactiveSprite;             
            }
            else
            {
                _spriteRenderer.sprite = _activeSprite; 
            }

            active = !active;
            _collider.enabled = active;
        }

        public void SetRewindTrue()
        {
            _rewinding = true;
        }

        public void SetRewindFalse()
        {
            _rewinding = false;
        }

        public void SetActive()
        {
            if(!active)
                Switch();
        }

        public void SetInactive()
        {
            if(active)
                Switch();
        }


        public void Start()
        {
            _spriteRenderer.sprite = _inactiveSprite;   
            _collider.enabled = false;          
            
        }

        public void Reseting()
        {
                _spriteRenderer.sprite = _inactiveSprite;   
                _collider.enabled = false;
                active = false;          
            
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.tag == "Player")
            {
                if(_rewinding)
                {
                    if(active)
                    {
                        Debug.Log("switching");
                        Switch();
                    }       
                }
            }
            
        }

    }
}
