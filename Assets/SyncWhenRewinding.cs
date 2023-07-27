using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncWhenRewinding : MonoBehaviour
{
    // Start is called before the first frame update
    private bool sync = false;
    private Vector2 beforRewindPosition;
    private Vector2 camBeforRewindPosition;
    private Transform _mainCam;
    private GameObject _player;

    private float _time = 0f;
    // Update is called once per frame
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
                _mainCam.position = _player.transform.position;
            }
            transform.position = beforRewindPosition;
            

            _time += Time.deltaTime;
        }
    }

    public void OnRewindSync()
    {
        _time = 0f;
        beforRewindPosition = transform.position;
        camBeforRewindPosition = _mainCam.position;
        sync = true;
    }

    public void StopSync()
    {
        sync = false;
    }
}
