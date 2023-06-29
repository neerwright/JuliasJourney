using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptables
{
    public class Vec2EventListeners : MonoBehaviour
    {
        public Vec2EventSO Event;
        public UnityEvent<Vector2> Response;

        private void OnEnable()
        { 
            if(Event)
                Event.RegisterListener(this); 
            else
            Debug.LogError("Event not set");        
        }

        private void OnDisable()
        { Event?.UnregisterListener(this); }

        public void OnEventRaised(Vector2 value)
        { 
            Debug.Log(value);
            Response.Invoke((Vector2) value); 
        }
    }
}