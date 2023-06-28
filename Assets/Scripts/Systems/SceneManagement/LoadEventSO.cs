using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
    [CreateAssetMenu]
    public class LoadEventSO : ScriptableObject
    {
        private List<LoadEventListeners> listeners = new List<LoadEventListeners>();

        public void RegisterListener(LoadEventListeners listener)
        {
            if(!(listeners.Contains(listener)))
                listeners.Add(listener);
        }

        public void UnregisterListener(LoadEventListeners listener)
        {
            if((listeners.Contains(listener)))
                listeners.Remove(listener);
        }

        public void Raise(GameSceneSO value)
        {
            for(int i = listeners.Count -1; i >= 0; i--)
                listeners[i]?.OnEventRaised(value);
        }

    }
}