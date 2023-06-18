using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class SpeedBelt : MonoBehaviour
{
    [SerializeField]
    private float speedBoost;

    [SerializeField]
    private bool speedRight;

    [SerializeField]
    private GameObject _player;

    private PlayerScript _playerScript; 
    // Start is called before the first frame update
    void Start()
    {
        _playerScript = _player.GetComponent<PlayerScript>();
    }

    private void OnTriggerStay2D(Collider2D Collider)
    {
        if(Collider.gameObject.tag == "Player")
        {    
            float dir = speedRight? 1 : -1;    
            _playerScript.movementVector.x += dir * Time.deltaTime * speedBoost;
        }
    }

    private void OnTriggerEnter2D(Collider2D Collider)
    {
        if(Collider.gameObject.tag == "Player")
        {                    
            _playerScript.movementVector.y += 0;
        }
    }
}
