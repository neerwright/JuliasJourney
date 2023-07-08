using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Scriptables;

namespace environment{

    public class Feather : MonoBehaviour , IEnvironmentalObject
    {

        [Header("Player")]
        [SerializeField]
        private float _speedBoost = 10f;
        [SerializeField]
        private GameEvent _playTrailParticles;
        
        [Header("Animation")]
        [SerializeField]
        private float _floatAnimationSpeed = 1f;
        [SerializeField]
        private AnimationCurve _curve;
        [SerializeField] private Sprite _upSprite;
        [SerializeField] private Sprite _downSprite;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private GameObject _player;
        private PlayerScript _playerScript;

        private float _current = 0f;
        private float _target = 1f;
        private Vector2 _startposition;
        private Vector2 _goalposition;
        private bool _floatUpwards = true;
        private const float FLOATING_ANIMATION_DISTANCE = 0.7f;
        private const float DELAY = 0.05f;

        private void Awake()
        {
            _startposition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - FLOATING_ANIMATION_DISTANCE);
            gameObject.transform.position = _startposition;

            _goalposition = _startposition;
            _goalposition.y += 2 * (FLOATING_ANIMATION_DISTANCE);
        }

        public void Initialize(GameObject player)
        {
            _player = player;
            _playerScript = _player.GetComponent<PlayerScript>();
        }

        private void OnTriggerEnter2D(Collider2D Collider)
        {
            if(Collider.gameObject.tag == "Player")
            {
                _playerScript.movementVector.x += _speedBoost * _playerScript.movementInput.x;
                _playTrailParticles.Raise();
            }
        }

        private void Update()
        {
            PlayFloatAnimation();
        }

        void PlayFloatAnimation()
        {
            
            _current = Mathf.MoveTowards(_current, _target, _floatAnimationSpeed * Time.deltaTime);



            gameObject.transform.position = Vector2.Lerp(_startposition, _goalposition, _curve.Evaluate(_current));
            if(_floatUpwards)
            {
                if(_current > 0.99)
                {
                    StartCoroutine("PlayAnimation");
                    _current = 1;
                    _floatUpwards = !_floatUpwards;
                    _target = 0;
                }
            }
            else
            {
                if(_current < 0.01)
                {
                    StartCoroutine("PlayAnimation");
                    _current = 0;
                    _floatUpwards = !_floatUpwards;
                    _target = 1;
                }
            }
            

        }

        IEnumerator PlayAnimation()
        {
            _spriteRenderer.sprite = _downSprite;
            yield return new WaitForSeconds(DELAY);
            _spriteRenderer.sprite = _upSprite;
        }
    }
}

