using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Player;
using Scriptables;

namespace environment
{
    public class SuperJumpPad : MonoBehaviour
    {
        public MMFeedbacks WobbleFeedback;
        public float speed = 100;
        public BoolVariableSO _touchedJumpPad;
        

        private void OnTriggerEnter2D(Collider2D Collider)
        {
            if(Collider.gameObject.tag == "Player")
            {
                var playerScript = Collider.gameObject.GetComponent<PlayerScript>();
                if(playerScript)
                {
                    print("got it");
                    _touchedJumpPad.Value = true;
                    playerScript.movementVector.y = speed;
                }
                
                if(!WobbleFeedback.Feedbacks[0].IsPlaying)
                    WobbleFeedback?.PlayFeedbacks();
            }
        }
    }
}
