using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu]
    public class Vec2EventSO : ScriptableObject
    {
        private List<Vec2EventListeners> listeners = new List<Vec2EventListeners>();

        public void RegisterListener(Vec2EventListeners listener)
        {
            if(!(listeners.Contains(listener)))
                listeners.Add(listener);
        }

        public void UnregisterListener(Vec2EventListeners listener)
        {
            if((listeners.Contains(listener)))
                listeners.Remove(listener);
        }

        public void Raise(Vector2 value)
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i]?.OnEventRaised((Vector2) value);
        }

    }
}