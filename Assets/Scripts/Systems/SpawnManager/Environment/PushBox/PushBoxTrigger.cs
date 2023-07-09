using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Sounds;

namespace environment
{
    public class PushBoxTrigger : MonoBehaviour 
    {
        [SerializeField] private PushBox _bushBox;

        private void OnTriggerEnter2D(Collider2D Collider)
        {
            if(Collider.gameObject.tag == "Player")
            {
                //_bushBox.OnImpact(Collider);
            }
        }

        public void Initialize(GameObject player)
        {
            //_bushBox.Initialize(player);
        }
    }
}