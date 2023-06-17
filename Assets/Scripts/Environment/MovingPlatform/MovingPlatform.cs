using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private int startingPoint = 0;
    [SerializeField]
    private Transform[] points;
    [SerializeField]
    private GameObject _player;

    private PlayerController _playerController;

    private int array_index;
    private bool playerIsTouchingPlatform = false;
    private const float REACH_GOAL_THRESHOLD = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
        _playerController = _player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Vector2.Distance(transform.position, points[array_index].position) < REACH_GOAL_THRESHOLD)
        {
            array_index++;
            if(array_index == points.Length)
            {
                array_index = 0;
            }
        }

        Vector3 oldPos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, points[array_index].position, speed * Time.deltaTime);
        

        if(playerIsTouchingPlatform)
        {
            Vector3 posDifference = transform.position - oldPos;    
        }
    }

    private void OnTriggerEnter2D(Collider2D Collider)
    {
        if(Collider.gameObject.tag == "Player")
        {
            playerIsTouchingPlatform = true;
            _playerController.TouchingPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D Collider)
    {
        if(Collider.gameObject.tag == "Player")
        {
            playerIsTouchingPlatform = false;
            _playerController.TouchingPlatform = false;
        }
    }
}
