using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scriptables;

namespace UI
{
    public class PocketwatchUI : MonoBehaviour
    {
        [SerializeField] private float _timeToFinish = 1.5f;
        [SerializeField] UIManager _uiManager;
        [SerializeField] Image _timerImage; 
        [SerializeField] Image _backgroundImage; 
        [SerializeField] Image _zeigerImage; 
        [SerializeField] AnimationCurve _curve; 
        [SerializeField]private Vector2VariableSO _playerPosition;
        [SerializeField] float _paddingY = 2f; 

        private Camera m_MainCamera;
        private float _timer;

        private bool _active = false;

        public void StartTimer()
        {
            if(_uiManager)
            {
                _uiManager.SetActice(CanvasType.PocketwatchUI, true);
            }
            _timer = _timeToFinish;
            _active = true;
        }

        public void StopTimer()
        {
            if(_uiManager)
            {
                _uiManager.SetActice(CanvasType.PocketwatchUI, false);
            }
            _timer = 0;
            _active = false;
        }

        private void Awake()
        {
            m_MainCamera = Camera.main;
            Debug.Log(m_MainCamera);
        }
        // Update is called once per frame
        void Update()
        {
            if(_active)
            {
                PositionAbovePlayer();
                UpdateTimer();
                float completeFraction = _timer / _timeToFinish;
                FadeInOut(_timerImage, completeFraction);
                FadeInOut(_backgroundImage, completeFraction);
                FadeInOut(_zeigerImage, completeFraction);
            }
            
        }

        void PositionAbovePlayer()
        {
            if(m_MainCamera)
            {
                Vector2 position = _playerPosition.Value;
                position.y += _paddingY;
                Vector2 screenPos = m_MainCamera.WorldToScreenPoint(position);
                _timerImage.transform.position = screenPos;
                _backgroundImage.transform.position = screenPos;
                _zeigerImage.transform.position = screenPos;

            }
        }

        void UpdateTimer()
        {
            _timer -= Time.deltaTime;
            _timerImage.fillAmount = _timer / _timeToFinish;
            if(_timer <= 0)
            {
                _active = false;
            }
        }

        void FadeInOut(Image image, float fraction)
        {    
            var tempColor = image.color;
            tempColor.a = Mathf.Lerp(0,1,_curve.Evaluate(fraction));
            image.color = tempColor;
        }



    }
}

