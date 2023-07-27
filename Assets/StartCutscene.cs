using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.SceneManagement;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject _camSetup;
    [SerializeField] private Transform _camCutsceneStartPoint;

    private Scene _island2;
    private bool _started = false;
    // Start is called before the first frame update
    void Start()
    {
        _camSetup.SetActive(false);
        _island2 = SceneManager.GetSceneByName("Island2");
    }

    public void StartTheCutscene()
    {
        Debug.Log("StartCutscene");
        _camSetup.SetActive(true);
        //Camera
        ProCamera2D.Instance.AddCameraTarget(_camCutsceneStartPoint, 1f, 1f, 0f);
    }
}
