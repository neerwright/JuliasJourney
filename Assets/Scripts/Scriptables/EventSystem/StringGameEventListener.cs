using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptables
{
    public class StringGameEventListener : MonoBehaviour
    {
        public StringGameEvent Event;
        public UnityEvent<string> Response;

        private void OnEnable()
        { 
            if(Event)
                Event.RegisterListener(this); 
            else
            Debug.LogError("Event not set");        
        }

        private void OnDisable()
        { Event?.UnregisterListener(this); }

        public void OnEventRaised(string value)
        { Response.Invoke(value); }
    }
}