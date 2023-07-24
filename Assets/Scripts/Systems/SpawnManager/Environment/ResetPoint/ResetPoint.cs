using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace environment
{
    public class ResetPoint : MonoBehaviour
    {
        [SerializeField] private Vec2EventSO _resetLocationEvent;
        [SerializeField] private GameObject _firePS;
        
        public void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.tag == "Player")
            {
                _firePS.SetActive(true);
                _resetLocationEvent.Raise((Vector2) gameObject.transform.position);
            }
                
        }


    }
}

