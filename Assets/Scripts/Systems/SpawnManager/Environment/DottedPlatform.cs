using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedPlatform : MonoBehaviour
{
    [SerializeField] private Collider2D _col;
    [SerializeField] private bool turnOff = false;
    
    void Start()
    {
        _col.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(!turnOff)
            {
                _col.enabled = true;
                
            }
            else
            {
                _col.enabled = false;
            }
        }
        
        
    }
}
