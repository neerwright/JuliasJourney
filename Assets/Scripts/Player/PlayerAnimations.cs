using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour
    {
    
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private ClipTransition _idle;
        [SerializeField] private ClipTransition _move;
        [SerializeField] private ClipTransition _slide;
        [SerializeField] private ClipTransition _jump;
        [SerializeField] private ClipTransition _walk;

        void Start()
        {

        }

        private void OnEnable()
        {
            _animancer.Play(_move);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
