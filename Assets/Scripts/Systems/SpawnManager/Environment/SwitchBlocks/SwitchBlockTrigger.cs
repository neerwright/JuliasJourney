using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

public class SwitchBlockTrigger : MonoBehaviour
{
    
    [SerializeField] private GameEvent _switchBlocksEvent;

    void OnTriggerEnter2D()
    {
        _switchBlocksEvent?.Raise();
    }
}
