using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class GlideTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D Collider)
    {
        if(Collider.gameObject.tag == "Player")
        {
            Debug.Log("glide");
            _playerController.isGliding = true;
        }
    }
}
