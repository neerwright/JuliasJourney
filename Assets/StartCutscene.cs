using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject _camSetup;
    [SerializeField] private Transform _camCutsceneStartPoint;
    // Start is called before the first frame update
    void Start()
    {
        _camSetup.SetActive(false);
    }

    // Update is called once per frame
    public void StartTheCutscene()
    {
        _camSetup.SetActive(true);
        //Camera
        ProCamera2D.Instance.AddCameraTarget(_camCutsceneStartPoint, 1f, 1f, 0f);
    }
}
