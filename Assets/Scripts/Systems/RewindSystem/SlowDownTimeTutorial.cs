using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace RewindSystem
{
    public class SlowDownTimeTutorial : MonoBehaviour
    {
        //[SerializeField] TutorialNPC _NPC;
        [SerializeField] private AnimationCurve _easeOut;
        [SerializeField] float _slowDownSpeed = 1f;
        [SerializeField] GameEvent startSlowDown;
        
        float _slowDownFactor = 1f;
        float _time = 0f;
        float _originalFixedDeltaTime;

        bool _slowDown = false;
        bool _raisedEvent = false;
        [SerializeField] bool _triggered = false;

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
                Time.fixedDeltaTime = _originalFixedDeltaTime;
                _slowDown = true;
                _triggered= true;
                
            }
                
        }
        public void OnReset()
        {
            _raisedEvent = false;
            _triggered = false;
            _time = 0f;
        }

        private void SlowDownTime()
        {
            if(_slowDownFactor < 0.6f && !_raisedEvent)
            {
                startSlowDown?.Raise();
                _raisedEvent = true;
            }
            if(_slowDownFactor < 0.1f)
            {
                
                _slowDown = false;
                //Time.fixedDeltaTime = 0f;
                Time.timeScale = 0f;
                return;
            }

            _slowDownFactor = _easeOut.Evaluate(_time);

            Time.timeScale = _slowDownFactor;
            Time.fixedDeltaTime = Time.timeScale *  _originalFixedDeltaTime;
            Mathf.Max(Time.fixedDeltaTime, _originalFixedDeltaTime / 2);
            

            _time += Time.deltaTime * _slowDownSpeed;
        }
    }
}