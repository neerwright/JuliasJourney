using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManagement;
using Scriptables;
using System.Linq;

namespace UI 
{
    public enum CanvasType
    {
        MainMenu,
        CreditsArt,
        Options,

        PauseMenu,
        PocketwatchUI
    }

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private bool _mainMenu = true;
        [SerializeField] private LoadEventSO _level1LoadEvent;
        [SerializeField] private GameSceneSO _level1ToLoad;

        [SerializeField] private GameEvent _exitGameEvent;
        [SerializeField] private GameEvent _resumeGameEvent;
        private List<CanvasController> _canvasControllers;
        private CanvasController _lastActiveCanvas;
        private CanvasController _pauseMenu;
        private CanvasController _pocketwatchUI;

        private bool _pauseMenueActive = false;

        void Awake()
        {
            if(_mainMenu)
            {
                _canvasControllers = GetComponentsInChildren<CanvasController>().ToList();

                _canvasControllers.ForEach(x => x.gameObject.SetActive(false));

                SwitchCanvas(CanvasType.MainMenu);
            }
            else
            {                
                _canvasControllers = GetComponentsInChildren<CanvasController>().ToList();
                _pauseMenu = _canvasControllers.Find(x => x.canvasType == CanvasType.PauseMenu);
                _pauseMenu.gameObject.SetActive(false);

                _pocketwatchUI = _canvasControllers.Find(x => x.canvasType == CanvasType.PocketwatchUI);
                _pocketwatchUI.gameObject.SetActive(false);
            }

            
        }

        public void SwitchCanvas(CanvasType type)
        {
            if(_lastActiveCanvas != null)
                _lastActiveCanvas.gameObject.SetActive(false);

            CanvasController newCanvasController = _canvasControllers.Find(x => x.canvasType == type);

            if(newCanvasController != null)
            {
                newCanvasController.gameObject.SetActive(true);
                _lastActiveCanvas = newCanvasController;
            }
        }

        public void SetActice(CanvasType type, bool value)
        {
            CanvasController newCanvasController = _canvasControllers.Find(x => x.canvasType == type);
            if(newCanvasController != null)
            {
                    newCanvasController.gameObject.SetActive(value);
   
            }
        }

        public void StartGame()
        {
            _level1LoadEvent?.Raise(_level1ToLoad);
        }

        public void ExitGame()
        {
            _exitGameEvent.Raise();
        }

        public void PauseGame()
        {
            if(_pauseMenueActive)
            {
                ResumeGame();
                return;
            }
                
            Debug.Log("_pauseMenu");
            if(_pauseMenu)
            {
                _pauseMenu.gameObject.SetActive(true);
            }
            _pauseMenueActive = !_pauseMenueActive;
        }

        public void ResumeGame()
        {
                Debug.Log("Resume");
            if(_pauseMenu)
            {
                _resumeGameEvent.Raise();
                _pauseMenu.gameObject.SetActive(false);
            }

            _pauseMenueActive = !_pauseMenueActive;
        }




    }
}


