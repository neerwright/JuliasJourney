using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace environment
{
    
    public class SpeedBeltTrigger : MonoBehaviour
    {
        public SpeedBelt _speedBelt;

        public void OnTriggerStay2D(Collider2D Collider)
                {
                    
                    if(Collider.gameObject.tag == "Player")
                    {    
                        _speedBelt.SpeedUpPlayer();
                        
                    }
                }
    }
}