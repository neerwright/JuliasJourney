using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sounds
{
    public class AudioClipGameEventListener : MonoBehaviour
    {
        public AudioClipGameEvent Event;
        public UnityEvent<AudioClip, float> Response;

        private void OnEnable()
        { 
            if(Event)
                Event.RegisterListener(this); 
            else
            Debug.LogError("Event not set");        
        }

        private void OnDisable()
        { Event?.UnregisterListener(this); }

        public void OnEventRaised(AudioClip value, float volume)
        { Response.Invoke(value, volume); }
    }
}