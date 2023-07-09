using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Sounds;

namespace environment
{
    public class PushBox : MonoBehaviour , IEnvironmentalObject
    {
        [Header("Movement")]
        [Range(0,1)]
        [SerializeField] private float _playerSlowDownPercent = 0.7f;
        [Range(0,1)]
        [SerializeField] private float _boxSlowDownPercent = 0.5f;

        [Header("Audio")]
        [SerializeField] private AudioClipGameEvent _audioClipGameEvent = default;
        [SerializeField] private AudioClip _activateClip;
        [SerializeField] private AudioClip _driveSoundsClip;
        [SerializeField] private float _volume = 1f;

        [Header("Animation")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _standingReadySprites;
        [SerializeField] private Sprite _activeReadySprites;
        [SerializeField] private Sprite _inactiveReadySprites;

        private GameObject _player;
        private PlayerScript _playerScript;
        private BoxCollider2D _collider;

        private Rigidbody2D _rb2d;

        private bool _moveRight = true;
        private Vector2 _startPosition;

        private bool _hadImpact = false;

        private const float THRESHOLD = 10f;
        private const float INACTIVE_THREASHOLD = 2f;

        public void Initialize(GameObject player)
        {
            _player = player;
            _playerScript = _player.GetComponent<PlayerScript>();
        }

        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _startPosition = gameObject.transform.position;
            _collider = GetComponent<BoxCollider2D>();
            if(_spriteRenderer != null)
                _spriteRenderer.sprite =_standingReadySprites;
        }

        void Update()
        {
            if(_hadImpact)
            {
                if(Mathf.Abs(_rb2d.velocity.x) < INACTIVE_THREASHOLD)
                    _spriteRenderer.sprite =_inactiveReadySprites;
            }
        }

        public void ReActivate()
        {
            _spriteRenderer.sprite =_standingReadySprites;
            _hadImpact = false;
        }
        public void Reset()
        {
            _spriteRenderer.sprite =_standingReadySprites;
            gameObject.transform.position = _startPosition;
            gameObject.transform.rotation = Quaternion.identity;
            _rb2d.velocity = Vector2.zero;
            _hadImpact = false;
        }

        private void OnTriggerEnter2D(Collider2D Collider)
        {
            Vector2 _playerImpact = _playerScript.movementVector;
            int mult = 1;
            if(!_moveRight)
                mult = -1;
            if(Collider.gameObject.tag == "Player" && (mult * _playerScript.movementVector.x > THRESHOLD) && !_hadImpact)
            {
               //play Audio
                _audioClipGameEvent.Raise(_activateClip, _volume);
                _spriteRenderer.sprite = _activeReadySprites;
                _hadImpact = true;

                Vector2 playerVel = _playerScript.movementVector;
                Vector2 relativeVelocity = playerVel + (Vector2) _rb2d.velocity;
                
                //squash player event

                //slow down player by 10 %
                _playerScript.movementVector = _playerImpact - (_playerImpact * _playerSlowDownPercent);

                //change sprite
                Vector2 impuls = relativeVelocity -  relativeVelocity * _boxSlowDownPercent;
                impuls.y = 0;
                _rb2d.AddForce(impuls, ForceMode2D.Impulse);

            }
        }

    }
}
