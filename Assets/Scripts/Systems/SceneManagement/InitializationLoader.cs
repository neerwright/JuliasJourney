using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Scriptables;
/// <summary>
/// This class is responsible for starting the game by loading the persistent managers scene 
/// and raising the event to load the Main Menu
/// </summary>
namespace SceneManagement
{
    public class InitializationLoader : MonoBehaviour
    {
        [SerializeField] private GameSceneSO _managersScene;
        [SerializeField] private GameSceneSO _menuToLoad;

        [Header("Broadcasting on")]
        [SerializeField] private LoadEventSO _menuLoadEvent;

        private void Awake()
        {
            SceneManager.sceneLoaded += LoadMainMenu;
        }

        void LoadMainMenu(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= LoadMainMenu;
            try
            {
                if (scene == SceneManager.GetSceneByName("InitScene"))
                {
                    print("InitScene Loaded...");
                }
            }
            catch (System.IndexOutOfRangeException s) { return; }

            
            SceneManager.LoadScene("PersistentManagers", LoadSceneMode.Additive);
            var persistentManagers = SceneManager.GetSceneByName("PersistentManagers");
            StartCoroutine(CheckIfSceneLoaded(persistentManagers)); 
        }

        IEnumerator SwitchActive()
        {
            var persistentManagers = SceneManager.GetSceneByName("PersistentManagers");
            if (persistentManagers.IsValid())
            {
                Debug.Log("Set Active");
                SceneManager.SetActiveScene(persistentManagers);
                
            } 
            Debug.Log("unload init Scene");    
            yield return SceneManager.UnloadSceneAsync("InitScene");
            
        }

        IEnumerator CheckIfSceneLoaded(Scene scene)
        {
            while (!scene.isLoaded) 
            {
                yield return new WaitForSeconds(0.1f);
            }
            print("Raise");
            _menuLoadEvent.Raise(_menuToLoad);
            StartCoroutine("SwitchActive");
            //var persistentManagers = SceneManager.GetSceneByName("PersistentManagers");
            //if (persistentManagers.IsValid())
            //{
            //    Debug.Log("Set Active");
            //    SceneManager.SetActiveScene(persistentManagers);
            //    
            //}
        }












/*
            var initScene = SceneManager.GetSceneByName("InitScene");
            
            SceneManager.sceneLoaded -= LoadMainMenu;
            
            _menuLoadEvent.Raise(_menuToLoad);
            
            SceneManager.SetActiveScene(initScene);
            if (initScene.IsValid()) 
            {
                //yield return SceneManager.UnloadSceneAsync(initScene);
            
                SceneManager.UnloadSceneAsync("InitScene"); //Initialization is the only scene in BuildSettings, thus it has index 0
            }
        }
        */
        
    }
}
