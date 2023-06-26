using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour
    {
    
        [SerializeField] private AnimancerComponent _animancer;
        [SerializeField] private AnimationClip _idle;
        [SerializeField] private AnimationClip _move;
        [SerializeField] private AnimationClip _slide;
        [SerializeField] private AnimationClip _jump;
        [SerializeField] private AnimationClip _walk;

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
