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
        private GameObjectSet _GameObjectsInsideTriggerBox;
            
        private void OnEnable()
            {
                if(_triggerBoxSet != null)
                {
                    foreach (Pair<BoolVariableSO, string> triggerBoxState in _triggerBoxSet.Items)
                    {
                        triggerBoxState.ItemOne.Value = false;
                    }
                }       
                else 
                    Debug.LogError("BoolVarSet not set in " + gameObject.name);

                if(_GameObjectsInsideTriggerBox == null)
                    Debug.LogError("GOSet not set in " + gameObject.name); 
                    
            }
    
            private void OnTriggerStay2D(Collider2D other)
            {
                Debug.Log(other.gameObject.tag);
                foreach (Pair<BoolVariableSO, string> triggerBoxState in _triggerBoxSet.Items)
                {
                    if(other.gameObject.tag == triggerBoxState.ItemTwo)
                    {
                        triggerBoxState.ItemOne.Value = true;
                        _GameObjectsInsideTriggerBox.Items.Add(other.gameObject);
                    }
                }   
            }

            private void OnTriggerExit2D(Collider2D other)
            {
                foreach (Pair<BoolVariableSO, string> triggerBoxState in _triggerBoxSet.Items)
                {
                    if(other.gameObject.tag == triggerBoxState.ItemTwo)
                    {
                        triggerBoxState.ItemOne.Value = false;
                        _GameObjectsInsideTriggerBox.Items.Remove(other.gameObject);
                    }
                }
            }  
             
    }
}
