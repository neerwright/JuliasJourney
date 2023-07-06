using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
namespace environment 
{
    public class SwitchBlockSwitch : MonoBehaviour
    {
        [SerializeField] private GameEvent _switchEvent;
        
        private bool _triggered = false;

        private const string PLAYER_TAG = "Player";
        private const string PUSHBOX_TAG = "PushBox";

        void OnTriggerEnter2D(Collider2D collider)
        {
            if(_triggered)
                return;

            if(collider.tag == PLAYER_TAG || collider.tag == PUSHBOX_TAG)
            {
                _switchEvent.Raise();
                _triggered = true;
            }
            
        }
    }
}

