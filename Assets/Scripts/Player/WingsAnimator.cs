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

        [Header("Up")]
        [SerializeField] private Sprite[] _upSprites;
        [SerializeField] private float _upSpeed = 5f;
        [SerializeField] private float _downpSpeed = 1f;
        [SerializeField] private Transform _targetBackOfPlayer;
        [SerializeField] private Vector3 _relativeStartPosition;
        
        [Header("Runp")]
        [SerializeField] private Sprite[] _runSprites;
        [SerializeField] private float _runSpeed = 5f;

        [SerializeField] private AnimationCurve _easeInOut;
        

        private WingAnimationState _state;
        private SpriteRenderer _spriteRenderer;
        private bool _playback = false;
        private int _index = 0;
        private float _time = 0;


        
        
        public void Deactivate()
        {
            _state = WingAnimationState.Inactive;
            _playback = false;
            _index = 0;
            _time = 0;
        }

        public void PlayIdle()
        {
            _state = WingAnimationState.Idle;
            _playback = false;
            _index = 0;
            _time = 0;
        }

        public void PlayUp()
        {
            _state = WingAnimationState.Up;
            _playback = false;
            _index = 0;
            _time = 0;
        }

        public void PlayDown()
        {
            _state = WingAnimationState.Down;
            _playback = true;
            if(_index >= _upSprites.Length)
                _index = _upSprites.Length - 1;
            _time = 0;
        }

        public void PlayRun()
        {
            _state = WingAnimationState.Run;
            _playback = false;
            _index = 0;
            _time = 0;
        }

        void Awake()
        {
            _state = WingAnimationState.Inactive;
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _relativeStartPosition = transform.localPosition;
        }

        void Start()
        {
        }

        void Update()
        {
            Debug.Log(_state);
            if(_state == WingAnimationState.Inactive)
            return;

            if(_state == WingAnimationState.Idle)
                Idle();

            if(_state == WingAnimationState.Up)
                UpAnim();

            if(_state == WingAnimationState.Down)
                DownAnim();

            if(_state == WingAnimationState.Run)
                RunAnim();

            

        }

        //playback needs to be true for reverse to work
        private void PlayAnimation(Sprite[] spriteArray, float speed, bool playback, bool reverse)
        {
            
            float current;
            if (reverse)
            {
                playback = true;
                if (_playback == false)
                {
                    return;
                }
            }
 
            if(_playback && playback)
            {
                current = 1f - _easeInOut.Evaluate(_time);

                if(current < (1f + (float) _index) / (float) (spriteArray.Length))
                {
                    _index--;
                    if(_index < 0)
                        _index = 0;
                    _spriteRenderer.sprite = spriteArray[_index];
                    Debug.Log(_index);
                }

                if(current < 0.001f)
                {
                    _playback = !_playback;
                    _time = 0f;
                }
            }
            else
            {
                current = _easeInOut.Evaluate(_time);

                if(current > ((1 + (float) _index) / (float)((spriteArray.Length))))
                {
                    _index++;
                    if(_index > spriteArray.Length -1)
                        _index = spriteArray.Length -1;

                    _spriteRenderer.sprite = spriteArray[_index];
                }

                if(current > 0.99f)
                {
                    _playback = !_playback;
                    _time = 0f;
                }

            }
        

            
            _time += Time.deltaTime * speed;
           

        }

        private void Idle()
        {
            PlayAnimation(_idleSprites, _idleSpeed, true, false);
        }
        
        private void UpAnim()
        {
            PlayAnimation(_upSprites, _upSpeed, false, false);
            MoveTowardsPlayer();
        }

        private void DownAnim()
        {
            PlayAnimation(_upSprites, _downpSpeed, true, true);
            MoveBack();
        }

        private void RunAnim()
        {
            PlayAnimation(_runSprites, _runSpeed, true, false);
            //MoveBack();
        }

        private void MoveTowardsPlayer()
        {
                Vector3 _relativeTargetPosition = _targetBackOfPlayer.localPosition;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _relativeTargetPosition , Time.deltaTime);         
        }

        private void MoveBack()
        {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _relativeStartPosition , Time.deltaTime * 0.1f);         
        }

            
        
    }

    

    public enum WingAnimationState
    {
        Inactive,
        Idle,
        Run,
        Up,
        Down,
        Boost,
        Glide
    }
}

