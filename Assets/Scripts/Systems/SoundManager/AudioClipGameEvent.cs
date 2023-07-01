using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu]
    public class AudioClipGameEvent : ScriptableObject
    {
        private List<AudioClipGameEventListener> listeners = new List<AudioClipGameEventListener>();

        public void RegisterListener(AudioClipGameEventListener listener)
        {
            if(!(listeners.Contains(listener)))
                listeners.Add(listener);
        }

        public void UnregisterListener(AudioClipGameEventListener listener)
        {
            if((listeners.Contains(listener)))
                listeners.Remove(listener);
        }

        public void Raise(AudioClip value, float volume)
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i]?.OnEventRaised(value, volume);
        }

    }
}