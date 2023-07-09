using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
using Player;

namespace environment
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SwitchPhysics : MonoBehaviour , IEnvironmentalObject
    {

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SwitchBlockSwitch _switchBlockSwitch;

        private GameObject _player;
        private PlayerScript _playerScript;
        private Vector2 _startPosition;
        private Quaternion _startRotation;
        private const string PLAYER_TAG = "Player";
        private const string PUSHBOX_TAG = "PushBox";

        private bool _rewinding = false;
        private const float REWIND_RESET_THREASHOLD = 16f;


        

        public void Initialize(GameObject player)
        {
            _player = player;
            _playerScript = _player.GetComponent<PlayerScript>();
        }

        void Start()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        void Update()
        {
            if(_rewinding)
            {
                Debug.Log("checkcheck");
                Debug.Log(Mathf.Abs(transform.position.x - _startRotation.x));
                if(Mathf.Abs(transform.position.x - _startRotation.x) < REWIND_RESET_THREASHOLD)
                {
                    
                    _switchBlockSwitch.Reseting();
                }
            }
        }

        public void Reset()
        {
            transform.position = _startPosition;
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            transform.rotation = _startRotation;
            
        }

        public  void StartRewinding()
        {
            _rewinding = true;
            _rb.velocity = Vector2.zero;
            
        }
        public  void StopRewinding()
        {
            _rewinding = false;
            
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.tag == PLAYER_TAG || collider.tag == PUSHBOX_TAG)
            {
                Vector2 dir = Vector2.zero;

                if(collider.tag == PLAYER_TAG)
                {
                    if(_playerScript != null)
                        dir = _playerScript.movementVector;
                }   
                else
                {
                    Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                    if(rb != null)
                    {
                        dir = rb.velocity;
                    }
                }

                if(dir.y < 0)
                    dir.y = 0;

                float angle = Vector2.Angle(dir, Vector2.right);
                _rb.AddForce(dir, ForceMode2D.Impulse);
                _rb.AddTorque(angle * Mathf.Deg2Rad, ForceMode2D.Impulse);
            }
        }
    }
}