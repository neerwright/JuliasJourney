using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace Player
{
    public class WriteRewindData : MonoBehaviour
    {
        [SerializeField] private Vector2VariableSO _velocitySO;
        
        [SerializeField] private PlayerScript _playerScript;
        // Update is called once per frame

        private bool _rewinding = false;

        void Update()
        {
            if(!_rewinding)
                _velocitySO.Value = _playerScript.movementVector;
            else
            {
                if(_velocitySO.Value != null)
                    _playerScript.movementVector = _velocitySO.Value;
            }
                
        }

        public void StartRewind()
        {
            _rewinding = true;
        }
        public void StopRewind()
        {
            _rewinding = false;
        }
    }
}

