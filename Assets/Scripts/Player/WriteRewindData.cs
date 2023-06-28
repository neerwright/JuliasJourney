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
        void Update()
        {
            _velocitySO.Value = _playerScript.movementVector;
        }
    }
}

