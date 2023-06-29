using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class LoadLevel1Event : MonoBehaviour
    {
        [SerializeField] private LoadEventSO _level1LoadEvent;
        [SerializeField] private GameSceneSO _levelToLoad;
        private bool raisedEvent = false;
        // Start is called before the first frame update
        void Update()
        {
            if(!raisedEvent && SceneManager.GetSceneByName("MainMenue").isLoaded)
            {
                raisedEvent = true;
                _level1LoadEvent?.Raise(_levelToLoad);
            }
                
        }

    }
}

