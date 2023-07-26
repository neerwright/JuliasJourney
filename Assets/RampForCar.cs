using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampForCar : MonoBehaviour
{
    [SerializeField] Vector2 Angle;
    [SerializeField] float _speedForPushBox;

    private const string PUSHBOX_TAG = "PushBox";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay2D(Collider2D Collider)
    {
        if(Collider.gameObject.tag == PUSHBOX_TAG)
        {
            
            var rb = Collider.GetComponent<Rigidbody2D>();
            if(rb)
                rb.AddForce(Angle * _speedForPushBox);
        }
    }
}
