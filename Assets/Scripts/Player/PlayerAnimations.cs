using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour
    {
    
        [SerializeField] private AnimancerComponent _animancer;
        //public ClipTransition _test;
        
        public AnimancerComponent get_animancer()
        {
            return _animancer;
        }

        private void Update()
        {
            //_animancer.Play(_test);
        }
    }
}
