
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
namespace environment 
{
    
    public class OnOffSprites : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _SpriteRenderer;
        [SerializeField] private Sprite _onSprites;
        [SerializeField] private Sprite _offSprites;
        

        void Start()
        {
            _SpriteRenderer.sprite = _offSprites;
        }
        
        public void turnOn()
        {
            _SpriteRenderer.sprite = _onSprites;
        }

        public void OnReset()
        {
            _SpriteRenderer.sprite = _offSprites;
        }

        public void turnOff()
        {
            _SpriteRenderer.sprite = _offSprites;
        }

    }
}
