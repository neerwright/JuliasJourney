using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace NPC
{
    public class TextWriter : MonoBehaviour
    {
        private TextMeshPro _text;
        private string _textToWrite;
        private float _timePerChar;
        private float _timer;
        private int _charIndex;

        public void SetText(string text)
        {
            _text.SetText(text);
        }

        public void AddWriter(TextMeshPro text, string textToWrite, float timePerChar)
        {
            _text = text;
            _textToWrite = textToWrite;
            _timePerChar = timePerChar;
            _charIndex = 0;

        }

        private void Update()
        {
            if(_text != null)
            {
                _timer -= Time.deltaTime;
                if(_timer <= 0f)
                {
                    _timer += _timePerChar;
                    _charIndex++;
                    _text.SetText(_textToWrite.Substring(0, _charIndex));

                    if(_charIndex >= _textToWrite.Length)
                    {
                        _text = null;
                        return;
                    }
                }
            }
        }
    
    }
}

