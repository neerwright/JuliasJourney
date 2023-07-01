using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptables
{
    public class FloatGameEventListener : MonoBehaviour
    {
        public FloatGameEvent Event;
        public UnityEvent<float> Response;

        private void OnEnable()
        { 
            if(Event)
                Event.RegisterListener(this); 
            else
            Debug.LogError("Event not set");        
        }

        private void OnDisable()
        { Event?.UnregisterListener(this); }

        public void OnEventRaised(float value)
        { Response.Invoke(value); }
    }
}