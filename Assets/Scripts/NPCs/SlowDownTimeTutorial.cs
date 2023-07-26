using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NPC
{
    public class SlowDownTimeTutorial : MonoBehaviour
    {
        //[SerializeField] TutorialNPC _NPC;
        [SerializeField] private AnimationCurve _easeOut;
        [SerializeField] float _slowDownSpeed = 2f;
        
        float _slowDownFactor = 1f;
        float _time = 0f;
        float _originalFixedDeltaTime;

        bool _slowDown = false;
        bool _triggered = false;

        public void StopSlowDownTime()
        {
            _slowDown = false;
            _slowDownFactor = 1;
            _time = 0f;
            Time.fixedDeltaTime = _originalFixedDeltaTime;
            Time.timeScale = 1f;

        }
        void Start()
        {
            _originalFixedDeltaTime = Time.fixedDeltaTime;
        }

        void Update()
        {
            if(!_slowDown)
                return;

            SlowDownTime();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.tag == "Player" && !_triggered)
            {
                _slowDown = true;
                _triggered= true;
            }
                
        }

        private void SlowDownTime()
        {
            if(_slowDownFactor < 0.01f)
            {
                _slowDown = false;
                Time.fixedDeltaTime = 0f;
                Time.timeScale = 0f;
                return;
            }

            _slowDownFactor = _easeOut.Evaluate(_time);

            Time.timeScale = _slowDownFactor;
            Time.fixedDeltaTime = Time.timeScale *  Time.fixedDeltaTime;

            

            _time += Time.deltaTime * _slowDownSpeed;
        }
    }
}