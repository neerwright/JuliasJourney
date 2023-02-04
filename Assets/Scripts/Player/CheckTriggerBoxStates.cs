using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace Player
{
    public class CheckTriggerBoxStates : MonoBehaviour
    {
        [SerializeField]
        private BoolStringVariableSet _triggerBoxSet;

        [SerializeField]
        private GameObjectVariableSO _lastGameObject;
            
        private void OnEnable()
            {
                if(_triggerBoxSet != null)
                {
                    foreach (Pair<BoolVariableSO, StringVariableSO> triggerBoxState in _triggerBoxSet.Items)
                    {
                        triggerBoxState.ItemOne.Value = false;
                    }
                }       
                else 
                    Debug.LogError("BoolVarSet not set in " + gameObject.name);

                if(_lastGameObject == null)
                    Debug.LogError("GOVar not set in " + gameObject.name); 
                    
            }
    
            private void OnTriggerEnter2D(Collider2D other)
            {
                foreach (Pair<BoolVariableSO, StringVariableSO> triggerBoxState in _triggerBoxSet.Items)
                {
                    if(other.gameObject.tag == triggerBoxState.ItemTwo.Value)
                    {
                        triggerBoxState.ItemOne.Value = true;
                        _lastGameObject.GameObject = other.gameObject;
                    }
                }
                
            }

            private void OnTriggerExit2D(Collider2D other)
            {
                foreach (Pair<BoolVariableSO, StringVariableSO> triggerBoxState in _triggerBoxSet.Items)
                {
                    if(other.gameObject.tag == triggerBoxState.ItemTwo.Value)
                    {
                        triggerBoxState.ItemOne.Value = false;
                    }
                }
            }  
             
    }
}
