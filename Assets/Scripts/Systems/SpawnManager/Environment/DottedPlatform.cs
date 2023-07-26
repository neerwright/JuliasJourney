using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedPlatform : MonoBehaviour
{
    [SerializeField] private Collider2D _col;
    
    void Start()
    {
        _col.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        _col.enabled = true;
        Debug.Log("Triggg");
    }
}
