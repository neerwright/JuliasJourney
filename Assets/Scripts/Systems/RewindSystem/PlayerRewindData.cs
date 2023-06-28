using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace Animancer.Assets.Scripts.Systems.RewindSystem
{

    public class PlayerRewindData : MonoBehaviour , IRewindData
    {
        
        // Start is called before the first frame update
        [SerializeField] private Vector2VariableSO _playerVelocity;

        public void Rewind(RecordedData data)
        {
            gameObject.transform.position = data.pos;
            _playerVelocity.Value = data.vel;
        }
    }
}