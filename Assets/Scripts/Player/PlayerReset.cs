using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerReset : MonoBehaviour
    {
        [SerializeField] PlayerInputSO _inputs;

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

        }
    }
}

