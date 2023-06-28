using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace RewindSystem
{

    public class RecordRewindData : MonoBehaviour
    {
        

        [SerializeField] private GameObject _rewindMethodUser;
        [SerializeField] private FloatVariableSO _playerVelocity;
        [SerializeField] GameEvent stopRewinding;

        [HideInInspector] public int RecordIndex {get;set;}
        
        private IRewindData _rewindMethod;
        private bool _rewind = false;
        private bool _stepedBack = false;

        private List<RecordedData> _recordedData;
        private const int maxRewindData = 300;
        private int index;

        public void StartRewind()
        {
            _rewind = true;
        }

        public void StopRewind()
        {
            _rewind = false;
            index = 1;
            _recordedData.Clear();
            stopRewinding?.Raise();
        }

        private void Awake()
        {
            _recordedData = new List<RecordedData>();
            _rewindMethod = _rewindMethodUser.GetComponent<IRewindData>();
        }


        void Update()
        {
            if(_rewind)
            {
                
                
                index--; 
                if(index <= 1)
                {
                    StopRewind();
                }
                else
                {
                    RecordedData data = _recordedData[index];
                    _rewindMethod.Rewind(data);
                }
                
                
                

            }
            else
            {                                
                //capture data
                    
                RecordedData data = new RecordedData();
                data.pos = gameObject.transform.position;
                data.vel = _playerVelocity.Value;
                _recordedData.Add(data);  
                int size = _recordedData.Count; 
                if(size > maxRewindData)
                {
                    _recordedData.RemoveAt(0);
                }       
                index = size -1;
            }
        }
        
    }
}

