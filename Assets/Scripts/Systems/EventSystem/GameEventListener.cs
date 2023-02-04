using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    { 
        if(Event)
            Event.RegisterListener(this); 
        else
        Debug.LogError("Event not set");        
    }

    private void OnDisable()
    { Event?.UnregisterListener(this); }

    public void OnEventRaised()
    { Response.Invoke(); }
}
