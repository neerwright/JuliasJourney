using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
namespace environment 
{
    
    public class ResetBlocksTrigger : MonoBehaviour
    {
        [SerializeField] private GameEvent _switchEvent;


        
        
        void OnTriggerEnter2D(Collider2D collider)
        {

            if(collider.tag == "Player")
            {
                _switchEvent.Raise();

            }
            
        }

    }
}