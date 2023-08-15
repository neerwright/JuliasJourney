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

        private float _icon1Delay;
        private float _icon2Delay;

        private Vector2 _padding = new Vector2(4f,0.8f);
        private const float TIME_PER_CHAR = 0.06f;

        private float _time = 0f;

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
            _time = 0f;
            if(_iconSpriteRenderer != null)
                _iconSpriteRenderer.enabled = (false);
            if(_icon2SpriteRenderer != null)
                _icon2SpriteRenderer.enabled = (false);

            
        }

        // Update is called once per frame
        void Update()
        {
            if(_active)
            {
                _backgroundSpriteRenderer.transform.localPosition = new Vector3((_backgroundSpriteRenderer.size.x / 2f) - 1.5f, 0f);
                Resize();

                _time += Time.unscaledDeltaTime;
                if(_time > _icon1Delay)
                {
                    if(_iconSpriteRenderer != null)
                        _iconSpriteRenderer.enabled = (true);
                }
                    

                if(_time > _icon2Delay)
                {
                    if(_icon2SpriteRenderer != null)
                        _icon2SpriteRenderer.enabled = (true);
                }
            }
        }

        public void Setup(string textToWrite, float delay1, float delay2)
        {
            _icon1Delay = delay1;
            _icon2Delay = delay2;
            
            Resize();
            _backgroundSpriteRenderer.transform.localPosition = new Vector3((_backgroundSpriteRenderer.size.x / 2f) - 1.5f, 0f);

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

