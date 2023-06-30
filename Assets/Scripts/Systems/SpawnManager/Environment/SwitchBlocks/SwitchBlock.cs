using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace environment
{
    public class SwitchBlock : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;
        [SerializeField] private Collider2D _collider;
        public bool active = false;
        
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

        public void Start()
        {
            if(!active)
            {
                _spriteRenderer.sprite = _inactiveSprite;   
                _collider.enabled = false;          
            }
        }

    }
}
