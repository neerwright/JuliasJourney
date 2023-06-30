using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

namespace Player
{
    public enum AnimancerObjects
    {
        Player,
        Arms
    }
    public class PlayerAnimations : MonoBehaviour
    {
    
        [SerializeField] private AnimancerComponent _playerAnimancer;
        [SerializeField] private AnimancerComponent _armsAnimancer;
        
        public AnimancerComponent get_animancer(AnimancerObjects animancerObject)
        {
            switch(animancerObject) 
            {
            case AnimancerObjects.Player:
                return _playerAnimancer;
                break;
            case AnimancerObjects.Arms:
                return _armsAnimancer;
                break;
            default:
                return _playerAnimancer;
            }
            
        }

    }
}
