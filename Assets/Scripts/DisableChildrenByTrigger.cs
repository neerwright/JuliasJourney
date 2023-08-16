using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableChildrenByTrigger : MonoBehaviour
{
    //[SerializeField] private float _maxDistance;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private bool _enable = false;

    private GameObject Player;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            _gameObject.SetActive(_enable); 

                    
            
        }
    }

}
