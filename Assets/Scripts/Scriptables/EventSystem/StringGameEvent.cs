using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scriptables
{
    [CreateAssetMenu]
    public class StringGameEvent : ScriptableObject
    {
        private List<StringGameEventListener> listeners = new List<StringGameEventListener>();

        public void RegisterListener(StringGameEventListener listener)
        {
            if(!(listeners.Contains(listener)))
                listeners.Add(listener);
        }

        public void UnregisterListener(StringGameEventListener listener)
        {
            if((listeners.Contains(listener)))
                listeners.Remove(listener);
        }

        public void Raise(string value)
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i]?.OnEventRaised(value);
        }

    }
}