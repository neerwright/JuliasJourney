using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NPC 
{
    public class ChatBubble : MonoBehaviour
    {
        [SerializeField] private GameObject _bubblePrefab;
        [SerializeField] TextWriter _textWriter;

        private SpriteRenderer _backgroundSpriteRenderer;
        private SpriteRenderer _iconSpriteRenderer;
        private SpriteRenderer _icon2SpriteRenderer;
        private TextMeshPro _text;

        private Vector2 _padding = new Vector2(0f,0f);
        private const float TIME_PER_CHAR = 0.1f;

        private bool _active = false;

        
        private void Awake()
        {
            _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
            _iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
            _icon2SpriteRenderer = transform.Find("Icon2").GetComponent<SpriteRenderer>();
            _text = transform.Find("Text").GetComponent<TextMeshPro>();
        }

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if(_active)
            {
                _backgroundSpriteRenderer.transform.localPosition = new Vector3(_backgroundSpriteRenderer.size.x / 2f, 0f);
                Resize();
            }
        }

        public void Setup(string textToWrite)
        {
            
            Resize();
            _backgroundSpriteRenderer.transform.localPosition = new Vector3(_backgroundSpriteRenderer.size.x / 2f, 0f);

            _textWriter.AddWriter(_text, textToWrite, TIME_PER_CHAR);


            _active = true;
        }

        private void Resize()
        {
            _text.ForceMeshUpdate();
            Vector2  textSize = _text.GetRenderedValues(false);
            
            _backgroundSpriteRenderer.size = textSize + _padding;
        }
    }
}

