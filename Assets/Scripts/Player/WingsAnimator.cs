using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

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
        

        [Header("Boost")]
        [SerializeField] private Sprite[] _boostSprites;
        [SerializeField] private float _boostpSpeed = 5f;

        [SerializeField] private MMFeedbacks _boostFeedback;
        
        [Header("Run")]
        [SerializeField] private Sprite[] _runSprites;
        [SerializeField] private float _runSpeed = 5f;

        [Header("Glide")]
        [SerializeField] private Sprite[] _glideSprites;
        [SerializeField] private float _glideSpeed = 1f;

        [SerializeField] private AnimationCurve _easeInOut;
        [SerializeField] private AnimationCurve _easeOut;
        [SerializeField] private AnimationCurve _glideCurve;
        

        private WingAnimationState _state;
        private SpriteRenderer _spriteRenderer;
        private bool _playback = false;
        private int _index = 0;
        private float _time = 0;
        private Vector3 _relativeStartPosition;

        private bool _boostAnimationPlaying = false;

        
        
        public void Deactivate()
        {
            _state = WingAnimationState.Inactive;
            _playback = false;
            _index = 0;
            _time = 0;
        }

        public void PlayIdle()
        {
            if(_boostAnimationPlaying)
                return;

            _state = WingAnimationState.Idle;
            _playback = false;
            _index = 0;
            _time = 0;
        }

        public void PlayUp()
        {
            if(_boostAnimationPlaying)
                return;

            _state = WingAnimationState.Up;
            _playback = false;
            _index = 0;
            _time = 0;
        }

        public void PlayDown()
        {
            if(_boostAnimationPlaying)
                return;
                
            _state = WingAnimationState.Down;
            _playback = true;
            if(_index >= _upSprites.Length)
                _index = _upSprites.Length - 1;
            _time = 0;
        }

        public void PlayBoost()
        {
            _boostAnimationPlaying = true;
            _state = WingAnimationState.Boost;
            _playback = false;
            _index = 0;
            _time = 0;

            if(!_boostFeedback.Feedbacks[0].IsPlaying)
                    _boostFeedback?.PlayFeedbacks();
        }

        public void PlayRun()
        {
            if(_boostAnimationPlaying)
                return;

            _state = WingAnimationState.Run;
            _playback = false;
            _index = 0;
            _time = 0;
        }

        public void PlayGlide()
        {
            _state = WingAnimationState.Glide;
            _playback = false;
            //_index = 0;
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
            //Debug.Log(_state);
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
            
            if(_state == WingAnimationState.Boost)
                BoostAnim();

            if(_state == WingAnimationState.Glide)
                GlideAnim();
            

        }

        //playback needs to be true for reverse to work
        private void PlayAnimation(Sprite[] spriteArray, float speed, bool playback, bool reverse , AnimationCurve curve)
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
                current = 1f - curve.Evaluate(_time);

                if(current < (1f + (float) _index) / (float) (spriteArray.Length))
                {
                    _index--;
                    if(_index < 0)
                        _index = 0;
                    _spriteRenderer.sprite = spriteArray[_index];
                }

                if(current < 0.001f)
                {
                    _playback = !_playback;
                    _time = 0f;
                }
            }
            else
            {
                current = curve.Evaluate(_time);

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
            PlayAnimation(_idleSprites, _idleSpeed, true, false, _easeInOut);
            MoveBack();
        }
        
        private void UpAnim()
        {
            PlayAnimation(_upSprites, _upSpeed, false, false, _easeInOut);
            MoveTowardsPlayer();
        }

        private void DownAnim()
        {
            PlayAnimation(_upSprites, _downpSpeed, true, true, _easeInOut);
            MoveTowardsPlayer();
        }

        private void RunAnim()
        {
            PlayAnimation(_runSprites, _runSpeed, true, false, _easeInOut);
            MoveBack();
        }

        private void BoostAnim()
        {               
                PlayAnimation(_boostSprites, _boostpSpeed, true, false, _easeOut);
                
                
                if (_index >= (_boostSprites.Length - 1))
                {
                    _boostAnimationPlaying = false;
                }
            
            MoveBack();
        }

        private void GlideAnim()
        {         
            Debug.Log(_index);      
            PlayAnimation(_glideSprites, _glideSpeed, false, false, _glideCurve);

            MoveBack();
        }



        private void MoveTowardsPlayer()
        {
                Vector3 _relativeTargetPosition = _targetBackOfPlayer.localPosition;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _relativeTargetPosition , Time.deltaTime * 1.5f);         
        }

        private void MoveBack()
        {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _relativeStartPosition , Time.deltaTime * 4.5f);         
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

