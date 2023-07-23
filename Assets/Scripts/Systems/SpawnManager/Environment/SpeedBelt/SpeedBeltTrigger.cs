using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace environment
{
    
    public class SpeedBeltTrigger : MonoBehaviour
    {
        public SpeedBelt _speedBelt;
        public float _speedForPushBox = 10000;

        private const string PLAYER_TAG = "Player";
        private const string PUSHBOX_TAG = "PushBox";

        public void OnTriggerStay2D(Collider2D Collider)
                {
                    print("trig");
                    if(Collider.gameObject.tag == PLAYER_TAG)
                    {    
                        _speedBelt.SpeedUpPlayer();
                        
                    }

                    if(Collider.gameObject.tag == PUSHBOX_TAG)
                    {
                        
                        var rb = Collider.GetComponent<Rigidbody2D>();
                        print(rb);
                        if(rb)
                            rb.AddForce(Vector2.right * Time.deltaTime * _speedForPushBox);
                    }
                }
    }
}