using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Fence : MonoBehaviour
{
    [SerializeField]
    private float speedThreashold;

    [SerializeField]
    private GameObject _player;

    private PlayerScript _playerScript; 
    // Start is called before the first frame update
    void Start()
    {
        _playerScript = _player.GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D Collider)
    {
        if(Collider.gameObject.tag == "Player")
        {
            if( _playerScript.movementVector.x > speedThreashold)
            {
                Destroy(gameObject);
            }
        }
    }
}
