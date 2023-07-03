using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player 
{
    public class WingsAnimator : MonoBehaviour
    {
        [Header("Idle")]
        [SerializeField] private Sprite[] _idleSprites;
        [SerializeField] private float _idleSpeed = 1f;

        [SerializeField] private AnimationCurve _easeInOut;
        

        private WingAnimationState _state;
        private SpriteRenderer _spriteRenderer;
        private bool _playback = false;
        private int _index = 0;
        private float _time = 0;

        public void PlayIdle()
        {
            _state = WingAnimationState.Idle;
            _playback = false;
            _index = 0;
            _time = 0;
        }

        void Awake()
        {
            _state = WingAnimationState.Inactive;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            Debug.Log("start");
            PlayIdle();
        }

        void Update()
        {
            if(_state == WingAnimationState.Inactive)
            return;

            if(_state == WingAnimationState.Idle)
                Idle();

            

        }

        private void Idle()
        {
            int mult;
            float current;


            

            if(_playback)
            {
                
                current = 1f - _easeInOut.Evaluate(_time);
                Debug.Log(current);

                if(current < (1f + (float) _index) / (float) (_idleSprites.Length))
                {
                    _index--;
                    if(_index < 0)
                        _index = 0;
                    _spriteRenderer.sprite = _idleSprites[_index];
                    Debug.Log(_index);
                }

                if(current < 0.001f)
                {
                    _playback = !_playback;
                    _time = 0f;
                    Debug.Log(_playback);
                }
            }
            else
            {
                current = _easeInOut.Evaluate(_time);

                if(current > ((1 + (float) _index) / (float)((_idleSprites.Length))))
                {
                    _index++;
                    if(_index > _idleSprites.Length -1)
                        _index = _idleSprites.Length -1;

                    _spriteRenderer.sprite = _idleSprites[_index];
                    Debug.Log(_index);
                }

                if(current > 0.99f)
                {
                    _playback = !_playback;
                    _time = 0f;
                    Debug.Log(_playback);
                }

            }


            _time += Time.deltaTime * _idleSpeed;
            //Debug.Log(current);
           

        }
    }

    public enum WingAnimationState
    {
        Inactive,
        Idle,
        Up,
        Down,
        Glide
    }
}

