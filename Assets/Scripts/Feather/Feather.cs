using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Feather : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _speedBoost = 10f;

    private PlayerScript _playerScript;

    private void Start()
    {
        _playerScript = _player.GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D Collider)
    {
        if(Collider.gameObject.tag == "Player")
        {
            _playerScript.movementVector.x += _speedBoost * _playerScript.movementInput.x;
        }
    }
}
