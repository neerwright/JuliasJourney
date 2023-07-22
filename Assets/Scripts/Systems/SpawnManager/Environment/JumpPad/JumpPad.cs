using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Scriptables;
using MoreMountains.Feedbacks;

namespace environment
{
    public class JumpPad : MonoBehaviour, IEnvironmentalObject
    {
        public MMFeedbacks WobbleFeedback;
        [SerializeField]
        private BoolVariableSO _touchedJumpPad;

        public void Initialize(GameObject player)
        {

        }
        private void OnTriggerEnter2D(Collider2D Collider)
        {
            if(Collider.gameObject.tag == "Player")
            {
                _touchedJumpPad.Value = true;
                
                if(!WobbleFeedback.Feedbacks[0].IsPlaying)
                    WobbleFeedback?.PlayFeedbacks();
            }
        }

    }
}