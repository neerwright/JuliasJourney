using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VariableSO;

public class CheckColliderWithTag : MonoBehaviour
{
    [SerializeField]
    private string _tag;

    [SerializeField]
    private BoolVariableSO _isInsideColliderWithTag;

    [SerializeField]
    private GameObjectVariableSO _lastGameObject;
        
     private void OnEnable()
        {
            if(_isInsideColliderWithTag != null)
                _isInsideColliderWithTag.Value = false;
            else 
                Debug.LogWarning("BoolVar not set in " + gameObject.name);

            if(_lastGameObject == null)
                Debug.LogWarning("GOVar not set in " + gameObject.name); 
                
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.tag == _tag)
            {
                if (_isInsideColliderWithTag)
                    _isInsideColliderWithTag.Value = true;
                    _lastGameObject.GameObject = other.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.tag == _tag)
            {
                if (_isInsideColliderWithTag)
                    _isInsideColliderWithTag.Value = false;
            }
        }   
}
