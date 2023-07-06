using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
    public class LoadIsland : MonoBehaviour
    {
        [SerializeField] private LoadEventSO _nextIslandEvent;
        [SerializeField] private GameSceneSO _nextIsland;

        [SerializeField] private bool _triggered = false;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.tag == "Player" && !_triggered)
            {
                _triggered = true;
                _nextIslandEvent?.Raise(_nextIsland);
            }
            
        }
    }
}

