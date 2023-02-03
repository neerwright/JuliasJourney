using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VariableSO;

namespace Environment{
    public class ExtraJump : MonoBehaviour
    {
        [SerializeField]
        private BoolVariableSO _isInsideCollider;
        
        private void OnEnable()
        {
            if(_isInsideCollider != null)
                _isInsideCollider.Value = false;
            else 
                Debug.LogWarning("BoolVar not set in " + gameObject.name);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("trig ");
            if(other.gameObject.tag == "Player")
            {
                if (_isInsideCollider)
                    _isInsideCollider.Value = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.tag == "Player")
            {
                if (_isInsideCollider)
                    _isInsideCollider.Value = false;
            }
        }
    }
}