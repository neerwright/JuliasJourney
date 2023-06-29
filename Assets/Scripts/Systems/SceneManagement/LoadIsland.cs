using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
    public class LoadIsland : MonoBehaviour
    {
        [SerializeField] private LoadEventSO _nextIslandEvent;
        [SerializeField] private GameSceneSO _nextIsland;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            _nextIslandEvent?.Raise(_nextIsland);
        }
    }
}

