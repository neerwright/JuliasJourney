using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableByDistance : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private GameObject _gameObject;

    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        StartCoroutine("DistanceCheck");
    }

    IEnumerator DistanceCheck()
    {
        while(true)
        {
            float dist = (transform.position - Player.transform.position).sqrMagnitude;

            if(dist < _maxDistance * _maxDistance) 
            {
                _gameObject.SetActive(true); 
            }
            else
            {
                _gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(1);
            }
        
    }
}
