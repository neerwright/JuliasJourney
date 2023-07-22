using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class BeltAnimation : MonoBehaviour
{
    [SerializeField] private AnimancerComponent _beltAnimancer;
    [SerializeField] private ClipTransition _clip;
    // Start is called before the first frame update
    void Start()
    {
        _beltAnimancer.Play(_clip);
    }

}
