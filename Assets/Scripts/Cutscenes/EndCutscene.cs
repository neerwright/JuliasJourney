using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.SceneManagement;
using NPC;
using Animancer;
using Player;
using GameManager;
using Sounds;

public enum EndCutsceneState
{
    started,
    moveToTable,
    playAnimations,
    ending
}

public class EndCutscene : MonoBehaviour
{
    //[SerializeField] private GameObject _player;
    [SerializeField] private GameObject _endPlayerModel;
    [SerializeField] private GameObject _blue;
    [SerializeField] private Sprite _blueSprite;
    [SerializeField] private AnimancerComponent _playerAnimancer;
    [SerializeField] private GameStateSO _gameState;

    [SerializeField] private AnimationCurve _easeOut;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform _tablePoint;

    //[SerializeField] private Transform _camFollowPoint;
    [Header("Audio")]
    [SerializeField] private AudioClipGameEvent _audioClipGameEvent = default;
    [SerializeField] private AudioClip _squeackClip;

    [Header("Animation")]
    [SerializeField] private ClipTransition _winkClip;
    [SerializeField] private ClipTransition _blushClip;
    //private AnimancerComponent _playerAnimancer;
    private AnimancerComponent _blueAnimancer;

    private EndCutsceneState state;
    private bool _startedPlayingAnim = false;


    private float current = 0;
    private Vector2 _startPos;

    void Start()
    {
        _endPlayerModel.SetActive(false);
        _startPos = _endPlayerModel.transform.position;

        SpriteRenderer _spriteRenderer = _blue.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _blueSprite;
    }

    void Update()
    {
        if(state == EndCutsceneState.ending)
            return;

        if( state == EndCutsceneState.moveToTable)
            ZoomToTable();


            

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            state = EndCutsceneState.started;
            PrepareCutscene(col.gameObject);
            _audioClipGameEvent.Raise(_squeackClip, 0.8f);
            state = EndCutsceneState.moveToTable;
        }
    }

    private void PrepareCutscene(GameObject go)
    {
        Debug.Log("Prepare");
        Debug.Log(go);
        _gameState.UpdateGameState(GameState.Cutscene);
        _endPlayerModel.SetActive(true);
        PlayerRendererController _rendController = go.GetComponent<PlayerRendererController>();
        _rendController.TurnRenderesOff();
        
        ProCamera2D.Instance.RemoveCameraTarget(go.transform);
        ProCamera2D.Instance.AddCameraTarget(_endPlayerModel.transform, 1f, 1f, 0f);

        PlayerScript _playerScript = go.GetComponent<PlayerScript>();
        _playerScript.DisableControls();
    }

    private void ZoomToTable()
    {
        
        
        current = Mathf.MoveTowards(current, 1f, _speed * Time.deltaTime);

        _endPlayerModel.transform.position = Vector2.Lerp(_startPos, _tablePoint.position, _easeOut.Evaluate(current));
        
        if(current > 0.98f)
        {
            state = EndCutsceneState.playAnimations;
            StartCoroutine("PlayAnims");
            Debug.Log("PlayAnimations");
        }
        
    }

    private IEnumerator PlayAnims()
    {
        Debug.Log("PlayAnimas1");
        _startedPlayingAnim = true;
        yield return new WaitForSeconds(1.5f);

        if(_playerAnimancer != null)
            _playerAnimancer.Play(_winkClip);
Debug.Log("PlayAnimas2");
        yield return new WaitForSeconds(2f);

        _blueAnimancer = _blue.GetComponent<AnimancerComponent>();
        if(_blueAnimancer != null)
            _blueAnimancer.Play(_blushClip);
Debug.Log("PlayAnimas3");

        yield return new WaitForSeconds(1f);

        state = EndCutsceneState.ending;


    }
}
