using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
//using Scriptables;

namespace environment
{
    public class PushBox : MonoBehaviour, IEnvironmentalObject
    {
        [Range(0,1)]
        [SerializeField] private float _playerSlowDownPercent = 0.7f;

        [Range(0,1)]
        [SerializeField] private float _boxSlowDownPercent = 0.5f;


        private GameObject _player;
        private PlayerScript _playerScript;
        private BoxCollider2D _collider;

        private Rigidbody2D _rb2d;

        private bool _moveRight = true;
        private Vector2 _startPosition;

        private bool _hadImpact = false;

        private const float THRESHOLD = 5f;

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
        }

        public void Reset()
        {
            gameObject.transform.position = _startPosition;
            gameObject.transform.rotation = Quaternion.identity;
            _rb2d.velocity = Vector2.zero;
            _hadImpact = false;
        }

        private void OnTriggerEnter2D(Collider2D Collider)
        {
            int mult = 1;
            if(!_moveRight)
                mult = -1;
            Debug.Log(_playerScript.movementVector.x);
            if(Collider.gameObject.tag == "Player" && (mult * _playerScript.movementVector.x > THRESHOLD) && !_hadImpact)
            {
               
                _hadImpact = true;

                Vector2 playerVel = _playerScript.movementVector;
                Vector2 relativeVelocity = playerVel + (Vector2) _rb2d.velocity;
                
                //squash player event
                //slow down player by 10 %
                _playerScript.movementVector = _playerScript.movementVector - (_playerScript.movementVector * _playerSlowDownPercent);

                //change sprite
                Vector2 impuls = relativeVelocity -  relativeVelocity * _boxSlowDownPercent;
                impuls.y = 0;
                _rb2d.AddForce(impuls, ForceMode2D.Impulse);

            }
        }

    }
}
