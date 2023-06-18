using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Scriptables;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    private BoolVariableSO _touchedJumpPad;


    private void OnTriggerEnter2D(Collider2D Collider)
    {
        if(Collider.gameObject.tag == "Player")
        {
            _touchedJumpPad.Value = true;
            
        }
    }

}
