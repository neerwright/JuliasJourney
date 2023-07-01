using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scriptables
{
    [CreateAssetMenu]
    public class FloatGameEvent : ScriptableObject
    {
        private List<FloatGameEventListener> listeners = new List<FloatGameEventListener>();

        public void RegisterListener(FloatGameEventListener listener)
        {
            if(!(listeners.Contains(listener)))
                listeners.Add(listener);
        }

        public void UnregisterListener(FloatGameEventListener listener)
        {
            if((listeners.Contains(listener)))
                listeners.Remove(listener);
        }

        public void Raise(float value)
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i]?.OnEventRaised(value);
        }

    }
}