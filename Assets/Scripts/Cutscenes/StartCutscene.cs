using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.SceneManagement;
using NPC;
using Animancer;
using Player;
using GameManager;

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
    [SerializeField] private GameStateSO _gameState;
    [SerializeField] private Transform _camCutsceneStartPoint;
    [SerializeField] private Transform _playerPoint;
    [SerializeField] private Transform _textPoint;

    [SerializeField] private AnimationCurve _easeInOut;
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private float _delay = 0.2f;

    [SerializeField] private ClipTransition _clip;
    private AnimancerComponent _animancer;

    private float current = 0;

    private Scene _island2;
    private bool _started = false;
    private StartCutsceneState state;
    private GameObject _player;
    private bool _startedPlayingAnim = false;

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

        //if( state == StartCutsceneState.playAnimation && !_startedPlayingAnim)
            

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

        GameObject player = GameObject.FindWithTag("Player");
        PlayerRendererController _rendController = player.GetComponent<PlayerRendererController>();
        yield return new WaitForSeconds(_delay);
        _rendController.TurnRenderesOff();
        yield return new WaitForSeconds(13f);
        Debug.Log("Remove target");
        ProCamera2D.Instance.RemoveCameraTarget(_camCutsceneStartPoint);
    }
    private IEnumerator TalkAndPlayAnim()
    {
        Debug.Log("textttt");
        _startedPlayingAnim = true;
        gameObject.GetComponent<BubbleCreator>().CreateBubble(_textPoint, Vector2.zero, "I have to get over there!");
        yield return new WaitForSeconds(1f);
        GameObject model = GameObject.FindWithTag("PlayerCutsceneModel");
        AnimancerComponent _animancer = model.GetComponent<AnimancerComponent>();
        if(_animancer != null)
            _animancer.Play(_clip);

        yield return new WaitForSeconds(2f);
        state = StartCutsceneState.ending;
        GivePlayerControl();

    }
    private void GivePlayerControl()
    {
        
        //turn off sprites, turn on real player anims
        GameObject model = GameObject.FindWithTag("PlayerCutsceneModel");
        
        model.SetActive(false);

        GameObject player = GameObject.FindWithTag("Player");
        ProCamera2D.Instance.AddCameraTarget(player.transform, 1f, 1f, 0f);
        
        PlayerRendererController _rendController = player.GetComponent<PlayerRendererController>();
        _rendController.TurnRenderesOn();
        PlayerScript _playerScript = player.GetComponent<PlayerScript>();
        _playerScript.EnableControls();

        _gameState.UpdateGameState(GameState.Gameplay);
        
    }
    private void ZoomToPlayer()
    {
        
        
        current = Mathf.MoveTowards(current, 1f, _speed * Time.deltaTime);

        _camCutsceneStartPoint.position = Vector2.Lerp(_startPos, _playerPoint.position, _easeInOut.Evaluate(current));
        
        if(current > 0.98f)
        {
            state = StartCutsceneState.playAnimation;
            StartCoroutine("TalkAndPlayAnim");
        }
        
    }
}
