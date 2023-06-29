using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

namespace Player
{
    public class ArmAnimation : MonoBehaviour
    {
        [SerializeField] AnimancerComponent a;

        [SerializeField] AnimationClip v;
        // Start is called before the first frame update
        void Start()
        {
            a.Play(v);
        }


    }
}

