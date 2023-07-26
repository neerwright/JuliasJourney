using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class NoJumpRamp : MonoBehaviour
    {
        [SerializeField] Vector2 Angle;
        [SerializeField] float _speedForPushBox;

        private const string PUSHBOX_TAG = "PushBox";
        private const string PLAYER = "Player";
        // Start is called before the first frame update
        void Start()
        {
            
        }

        void OnTriggerEnter2D(Collider2D Collider)
        {
            if(Collider.gameObject.tag == PLAYER)
            {
                Collider.gameObject.GetComponent<PlayerScript>().PreventJumping = true;
            }
        }
        void OnTriggerExit2D(Collider2D Collider)
        {
            if(Collider.gameObject.tag == PLAYER)
            {
                Collider.gameObject.GetComponent<PlayerScript>().PreventJumping = false;
            }
        }

        void OnTriggerStay2D(Collider2D Collider)
        {
            
            if(Collider.gameObject.tag == PUSHBOX_TAG)
            {
                
                var rb = Collider.GetComponent<Rigidbody2D>();
                if(rb)
                    rb.AddForce(Angle * _speedForPushBox);
            }
        }
    }
}