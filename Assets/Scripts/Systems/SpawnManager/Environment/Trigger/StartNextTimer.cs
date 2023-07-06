using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace environment
{
    public class StartNextTimer : MonoBehaviour
    {
        [SerializeField] GameEvent _resetTimerEvent;
        [SerializeField] StringGameEvent _startTimerEvent;
        [SerializeField] string _islandName;
        
        private bool _trigerred = false;
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.tag == "Player")
            {
                if(!_trigerred)
                {
                    _startTimerEvent.Raise(_islandName);
                }
                else
                {
                    _resetTimerEvent.Raise();
                }
            }
        }
    }
}

