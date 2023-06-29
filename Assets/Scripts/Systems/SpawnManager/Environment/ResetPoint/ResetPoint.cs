using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace environment
{
    public class ResetPoint : MonoBehaviour
    {
        [SerializeField] private Vec2EventSO _resetLocationEvent;
        
        public void Start()
        {
            _resetLocationEvent.Raise((Vector2) gameObject.transform.position);
        }


    }
}

