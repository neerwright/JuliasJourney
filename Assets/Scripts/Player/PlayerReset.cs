using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using Com.LuisPedroFonseca.ProCamera2D;
using Scriptables;

namespace Player
{
    public class PlayerReset : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO _inputs;
        [SerializeField] private GameStateSO _gameState;
        [SerializeField] private GameObject _playerModel;
        [SerializeField] private float _delay = 0.3f;
        [SerializeField] private float _fadeIn = 0.3f;
        [SerializeField] private GameEvent _resetEvent;
        private bool _isResetting = false;

        [Header("PlayerMaterials")]
        [SerializeField] List<Material > PlayerMaterials;


        private PlayerScript _playerScript;


        private Vector2 _resetLocation;
        private const float ALPHA_INCREMENT = 50.0f;

        void Awake()
        {
            _playerScript = GetComponent<PlayerScript>();
        }

        private void OnEnable()
        {
            _inputs.ResetEvent += OnPlayerReset;
        }

        private void OnDisable()
        {
            _inputs.ResetEvent -= OnPlayerReset;
        }

        private void OnPlayerReset()
        {
            if(_isResetting)
                return;
            
        
            if(_gameState.CurrentGameState == GameState.Gameplay)
            {
                _resetEvent.Raise();
                _playerScript.DisableControls();
                _playerModel.SetActive(false);
                gameObject.SetActive(false);
                _playerScript.movementVector = Vector2.zero;
                gameObject.transform.position = _resetLocation;
                gameObject.SetActive(true);
                ProCamera2D.Instance.MoveCameraInstantlyToPosition(_resetLocation);
                _isResetting = true;

                foreach(var material in PlayerMaterials)
                {
                    Color tmp = material.color;
                    tmp.a = 0;
                    material.color = tmp;
                }
                StartCoroutine(waitToReEnablePlayer(_delay));
                
            }
        }

        public void SetNewResetLocation(Vector2 resetLocation)
        {
            _resetLocation = resetLocation;
        }

        IEnumerator waitToReEnablePlayer(float sec)
        {
            yield return new WaitForSeconds(sec);
            _playerModel.SetActive(true);

            StartCoroutine("fadeIn");
            
        }

        IEnumerator fadeIn()
        {
            while(PlayerMaterials[0].color.a < 1)
            {
                foreach(var material in PlayerMaterials)
                {
                    Color tmp = material.color;
                    tmp.a += ALPHA_INCREMENT * Time.deltaTime;
                    material.color = tmp;
                }
                yield return new WaitForSeconds(_fadeIn);
            }
            
            foreach(var material in PlayerMaterials)
                {
                    Color tmp = material.color;
                    tmp.a = 1;
                    material.color = tmp;
                }
            _isResetting = false;
            _playerScript.EnableControls();
        }

    }
}

