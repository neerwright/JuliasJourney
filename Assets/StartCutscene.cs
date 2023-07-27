using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.SceneManagement;

public enum StartCutsceneState
{
    started,
    moveToPlayer,
    playAnimation,
    ending
}
public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject _camSetup;
    [SerializeField] private Transform _camCutsceneStartPoint;
    [SerializeField] private Transform _playerPoint;

    [SerializeField] private AnimationCurve _easeInOut;
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private float _delay = 0.2f;

    private float current = 0;

    private Scene _island2;
    private bool _started = false;
    private StartCutsceneState state;
    private GameObject _player;

    private Vector2 _startPos;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = _camCutsceneStartPoint.position;
        state = StartCutsceneState.started;
        _camSetup.SetActive(false);
        _island2 = SceneManager.GetSceneByName("Island2");
    }

    void Update()
    {
        if( state == StartCutsceneState.moveToPlayer)
            ZoomToPlayer();
    }

    
    public void StartTheCutscene()
    {
        _player = GameObject.FindWithTag("Player");
        StartCoroutine("WaitBeforeStarting");
        
    }

    private IEnumerator WaitBeforeStarting()
    {
        yield return new WaitForSeconds(_delay);
        //Camera
        _camSetup.SetActive(true);
        ProCamera2D.Instance.AddCameraTarget(_camCutsceneStartPoint, 1f, 1f, 0f);
        yield return new WaitForSeconds(_delay);
        state = StartCutsceneState.moveToPlayer;
    }

    private void ZoomToPlayer()
    {
        
        
        current = Mathf.MoveTowards(current, 1f, _speed * Time.deltaTime);

        _camCutsceneStartPoint.position = Vector2.Lerp(_startPos, _playerPoint.position, _easeInOut.Evaluate(current));
        Debug.Log(current);
        if(current > 0.95f)
            {
                state = StartCutsceneState.playAnimation;
            }
        
    }
}
