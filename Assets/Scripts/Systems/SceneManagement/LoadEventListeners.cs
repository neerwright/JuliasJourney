using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SceneManagement
{
    public class LoadEventListeners : MonoBehaviour
    {
        public LoadEventSO Event;
        public UnityEvent<GameSceneSO> Response;

        private void OnEnable()
        { 
            if(Event)
                Event.RegisterListener(this); 
            else
            Debug.LogError("Event not set");        
        }

        private void OnDisable()
        { Event?.UnregisterListener(this); }

        public void OnEventRaised(GameSceneSO value)
        { Response.Invoke(value); }
    }
}