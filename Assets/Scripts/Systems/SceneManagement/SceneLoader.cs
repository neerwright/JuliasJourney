using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Scriptables;
using System;

/// <summary>
/// This class manages the scene loading and unloading.
/// </summary>
namespace SceneManagement
{

    public class SceneLoader : MonoBehaviour
    {
        //[SerializeField] private GameSceneSO _gameplayScene = default;
        
        //[SerializeField] private InputReader _inputReader = default;

        //[Header("Listening to")]
        //[SerializeField] private LoadEventListener _loadLocation = default;
        //[SerializeField] private LoadEventChannelSO _loadMenu = default;
        //[SerializeField] private LoadEventChannelSO _coldStartupLocation = default;

        [Header("Broadcasting on")]
        [SerializeField] private GameEvent _onSceneReady = default; //picked up by the SpawnSystem
        
        
        [SerializeField] private GameSceneSO _gameplayScene;
        //[SerializeField] private FadeChannelSO _fadeRequestChannel = default;

        public event EventHandler _gamplaySceneLoaded = delegate {};
        public event EventHandler _onNewSceneLoaded = delegate {};
        //private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;

        //Parameters coming from scene loading requests
        private GameSceneSO _sceneToLoad;
        private GameSceneSO _currentlyLoadedScene;
        //private bool _showLoadingScreen;

        private Scene _gameplayManagerScene = new Scene();
        //private float _fadeDuration = .5f;
        private bool _isLoading = false; //To prevent a new loading request while already loading a new scene

        private void OnEnable()
        {
            _gamplaySceneLoaded = OnGameplayManagersLoaded;
            _onNewSceneLoaded = OnNewSceneLoaded;
            //_loadLocation.OnLoadingRequested += LoadLocation;
            //_loadMenu.OnLoadingRequested += LoadMenu;
        }
        private void OnDisable()
	    {
            _gamplaySceneLoaded -= OnGameplayManagersLoaded;
            _onNewSceneLoaded -= OnNewSceneLoaded;
        }
        
        public void LoadLocation(GameSceneSO locationToLoad)
        {
            //Prevent a double-loading, for situations where the player falls in two Exit colliders in one frame
            if (_isLoading)
            {
                StartCoroutine(WaitToLoadLocationNext(locationToLoad));
                return;
            }

            _sceneToLoad = locationToLoad;
            _isLoading = true;

            Scene gameplayScene = SceneManager.GetSceneByName(_gameplayScene.sceneName);
            //In case we are coming from the main menu, we need to load the Gameplay manager scene first
            if (!gameplayScene.isLoaded)
            {
                Debug.Log("Load gameplay");
                SceneManager.LoadSceneAsync(_gameplayScene.sceneName, LoadSceneMode.Additive);
                IEnumerator couroutine = CheckIfGameplayLoaded(_gameplayScene);
                StartCoroutine(couroutine);
            }
            else
            {
                //StartCoroutine("UnloadPreviousScene");
                LoadNewScene();
            }
        }

        private IEnumerator WaitToLoadMenueNext(GameSceneSO menuToLoad)
        {
            while (_isLoading) 
            {
                yield return new WaitForSeconds(0.1f);
            }
            LoadMenu(menuToLoad);
        }

        private IEnumerator WaitToLoadLocationNext(GameSceneSO locationToLoad)
        {
            while (_isLoading) 
            {
                yield return new WaitForSeconds(0.1f);
            }
            LoadLocation(locationToLoad);
        }


        public void LoadMenu(GameSceneSO menuToLoad)
        {
            Debug.Log("load menue");
            //Prevent a double-loading, for situations where the player falls in two Exit colliders in one frame
            if (_isLoading)
            {
                StartCoroutine(WaitToLoadMenueNext(menuToLoad));
                return;
            }
                

            _isLoading = true;
            _sceneToLoad = menuToLoad;

            //In case we are coming from a Location back to the main menu, we need to get rid of the persistent Gameplay manager scene
            Scene gameplayScene = SceneManager.GetSceneByName("Gameplay");
            if (gameplayScene.isLoaded)
                SceneManager.UnloadSceneAsync(gameplayScene);

            //StartCoroutine("UnloadPreviousScene");
            LoadNewScene();
            
        }

        IEnumerator CheckIfGameplayLoaded(GameSceneSO gameScene)
        {
            Scene scene = SceneManager.GetSceneByName(gameScene.sceneName);
            while (!scene.isLoaded) 
            {
                yield return new WaitForSeconds(0.1f);
            }
            _gamplaySceneLoaded?.Invoke(this, EventArgs.Empty);
            
        }

        IEnumerator CheckIfSceneLoaded(GameSceneSO gameScene)
        {
            Scene scene = SceneManager.GetSceneByName(gameScene.sceneName);
            while (!scene.isLoaded) 
            {
                Debug.Log(gameScene.sceneName);
                yield return new WaitForSeconds(0.1f);
            }
            Debug.Log("Invoke");
            _onNewSceneLoaded.Invoke(this, EventArgs.Empty);
            
        }

        private void OnGameplayManagersLoaded(object sender, EventArgs e)
        {
            Debug.Log("1");
            //UnloadPreviousScene();
            LoadNewScene();
        }


        private void UnloadPreviousScene()
        {
            //_inputReader.DisableAllInput();
            Debug.Log("del");
            if (_currentlyLoadedScene != null) //would be null if the game was started in Initialisation
            {
                
                if (SceneManager.GetSceneByName(_currentlyLoadedScene.sceneName).IsValid()) 
                {
                    
                    Debug.Log(_currentlyLoadedScene);
                    Debug.Log("/////////");
                    SceneManager.UnloadSceneAsync(_currentlyLoadedScene.sceneName);
                }
                
                
                
            }




        }


        private void LoadNewScene()
        {
            Debug.Log("2");
            Debug.Log(_sceneToLoad.sceneName);
            Debug.Log("/////////");
            Scene scene = SceneManager.GetSceneByName(_sceneToLoad.sceneName);
            SceneManager.LoadSceneAsync(_sceneToLoad.sceneName, LoadSceneMode.Additive);
            StartCoroutine(CheckIfSceneLoaded(_sceneToLoad));
        }

        private void OnNewSceneLoaded(object sender, EventArgs e)
        {
            Debug.Log("3");
            Debug.Log(_currentlyLoadedScene);
            Debug.Log(_sceneToLoad);
            Debug.Log("----------------");
            UnloadPreviousScene();
            //Save loaded scenes (to be unloaded at next load request)
            _currentlyLoadedScene = _sceneToLoad;
            Scene scene = SceneManager.GetSceneByName(_currentlyLoadedScene.sceneName);
            if(scene.IsValid())
            {
                SceneManager.SetActiveScene(scene);
            }
            

            _isLoading = false;


            StartGameplay();
        }

        private void StartGameplay()
        {
            _onSceneReady.Raise(); //Spawn system will spawn the Player in a gameplay scene
        }

        private void ExitGame()
        {
            Application.Quit();
            Debug.Log("Exit!");
        }
    }
}