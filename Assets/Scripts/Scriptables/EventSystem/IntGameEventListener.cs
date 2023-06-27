using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptables
{
    public class IntGameEventListener : MonoBehaviour
    {
        public IntGameEvent Event;
        public UnityEvent<int> Response;

        private void OnEnable()
        { 
            if(Event)
                Event.RegisterListener(this); 
            else
            Debug.LogError("Event not set");        
        }

        private void OnDisable()
        { Event?.UnregisterListener(this); }

        public void OnEventRaised(int value)
        { Response.Invoke(value); }
    }
}