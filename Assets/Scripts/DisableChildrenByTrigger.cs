using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableChildrenByTrigger : MonoBehaviour
{
    //[SerializeField] private float _maxDistance;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private bool _enable = false;

    private GameObject Player;

    void Start()
    {
        //Player = GameObject.FindWithTag("Player");
        //StartCoroutine("DistanceCheck");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            _gameObject.SetActive(_enable); 
            //int children = _gameObject.transform.childCount;
            //for (int i = 0; i < children; ++i)
            //{

            //    Transform child = transform.GetChild(i);
             //   child.gameObject.SetActive(_enable); 
            //    Debug.Log()
            //}
                    
            
        }
    }

}
