using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace RewindSystem
{

    public class PlayerRewindData : MonoBehaviour , IRewindData
    {
        
        // Start is called before the first frame update
        [SerializeField] private Vector2VariableSO _playerVelocity;
        [SerializeField] private AnimationTrackerSO _animationData;

        public void Rewind(RecordedData data)
        {
            gameObject.transform.position = data.pos;
            _playerVelocity.Value = data.vel;

            _animationData.Time = data.time;
            _animationData.Clip = data.clip;
        }
    }
}