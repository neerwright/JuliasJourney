using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
public class SyncWhenRewinding : MonoBehaviour
{
    // Start is called before the first frame update
    private bool sync = false;
    private Vector2 beforRewindPosition;
    private Vector2 camBeforRewindPosition;
    private Transform _mainCam;
    private GameObject _player;
    private ProCamera2DZoomToFitTargets _instance;
    private float _time = 0f;
    // Update is called once per frame
    private float sizeBeforeRewind;
    void Start()
    {
        _mainCam = Camera.main.gameObject.transform;
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(sync && _time < 0.1f)
        {
            if(_player != null)
            {
                Debug.Log("not null");
                //Vector2 posi = _player.transform.position;
                //posi.x += 10f;
                //_mainCam.position = posi ;
                ProCamera2D.Instance.MoveCameraInstantlyToPosition(_player.transform.position);
            }
            //_mainCam.position = camBeforRewindPosition;
            //transform.position = beforRewindPosition;
            //_instance.DisableWhenOneTarget = true;
            //Camera.main.orthographicSize = 15f;
            _time += Time.deltaTime;
            
        }
    }

    public void OnRewindSync()
    {
        _instance = (ProCamera2DZoomToFitTargets) GameObject.FindObjectOfType(typeof(ProCamera2DZoomToFitTargets));
        if(_player == null)
        {
            _player = GameObject.FindWithTag("Player");
        }
        _time = 0f;
        beforRewindPosition = transform.position;
        camBeforRewindPosition = _mainCam.position;
        sizeBeforeRewind = Camera.main.orthographicSize;
        sync = true;
        //ProCamera2D.Instance.MoveCameraInstantlyToPosition(_player.transform.position);
        //ProCamera2D.Instance.RemoveCameraTarget(_player.transform);
    }

    public void StopSync()
    {
        sync = false;
    }
}
