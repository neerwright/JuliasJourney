using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace Player
{
    public class TrackVelocity : MonoBehaviour
    {
        [SerializeField] private Vector2VariableSO velocitySO;
        [SerializeField] private PlayerScript _playerScript;
        // Update is called once per frame
        void Update()
        {
            velocitySO.Value = _playerScript.movementVector;
        }
    }
}

