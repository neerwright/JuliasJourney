using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
/// <summary>
/// This class is responsible for starting the game by loading the persistent managers scene 
/// and raising the event to load the Main Menu
/// </summary>
namespace SceneManagement
{
    public class InitializationLoader : MonoBehaviour
    {
        [SerializeField] private GameSceneSO _managersScene = default;
        [SerializeField] private GameSceneSO _menuToLoad = default;

        [Header("Broadcasting on")]
        [SerializeField] private LoadEventSO _menuLoadEvent;

        private void Awake()
        {
            SceneManager.sceneLoaded += LoadMainMenu;
        }
        private void Start()
        {
            //SceneManager.LoadSceneAsync(_managersScene.sceneName, LoadSceneMode.Additive);
        }

        //private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
        //{
        //    _menuLoadChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += LoadMainMenu;
        //}

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
            StartCoroutine("SwitchActive"); // <--- using Lariosss's answer
        }

        IEnumerator SwitchActive()
        {
            var persistentManagers = SceneManager.GetSceneByName("PersistentManagers");
            SceneManager.LoadScene("PersistentManagers", LoadSceneMode.Single);
            if (persistentManagers.IsValid()) 
                SceneManager.SetActiveScene(persistentManagers);
            yield return SceneManager.UnloadSceneAsync("InitScene");
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
