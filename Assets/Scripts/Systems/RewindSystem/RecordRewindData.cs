using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
using System;

namespace RewindSystem
{

    public class RecordRewindData : MonoBehaviour
    {
        

        [SerializeField] private GameObject _rewindMethodUser;
        [SerializeField] private Vector2VariableSO _playerVelocity;
        [SerializeField] private AnimationTrackerSO _animationData;
        [SerializeField] private bool _recordPlayerData = true;
        
        //[SerializeField] private bool _useArray = false;

        [HideInInspector] public int RecordIndex {get;set;}
        
        private IRewindData _rewindMethod;
        private bool _rewind = false;
        private bool _stepedBack = false;

        private List<RecordedData> _recordedData;
        private const int MAX_REWIND_DATA = 2000;
        private const float MAX_REWIND_TIME = 1.1f;
        private int index = 0;

        private RecordedData[,] _recordedDataArray;
        private RewindDataManager _rewindDataManager;

        public void StartRewind()
        {
            _rewind = true;

			var coroutine = RewindTimer(MAX_REWIND_TIME);
        	StartCoroutine(coroutine);
			
        }

        

        private void Awake()
        {
            _rewindDataManager = new RewindDataManager(true, MAX_REWIND_DATA);
            
            _rewindMethod = _rewindMethodUser.GetComponent<IRewindData>();
        }

        public void StopRewind()
        {
            _rewind = false;
            index = 0;
            _rewindDataManager?.Clear();
        }


        void Update()
        {
            if(_rewind)
            {
                
                if(index >= MAX_REWIND_DATA)
                {
                    index = MAX_REWIND_DATA -1;
                } 

                ; 
                if(index <= 0)
                {
                    StopRewind();
                    return;
                }
                else
                {
                    RecordedData data = _rewindDataManager.Get(index);
                    _rewindMethod.Rewind(data);
                }
                index--;
                
                

            }
            else
            {                                
                //capture data
                    
                RecordedData data = new RecordedData();
                data.pos = gameObject.transform.position;
                if(_recordPlayerData)
                {
                    data.vel = _playerVelocity.Value;
                    data.time = _animationData.Time;
                    data.clip = _animationData.Clip;
                }
                
                _rewindDataManager.Enqueue(data);

                //int size = _rewindDataManager.Size();
                if(index > MAX_REWIND_DATA)
                {
                    _rewindDataManager.Dequeue();
                    index = MAX_REWIND_DATA -1;
                }       
                index++;
            }
        }

        IEnumerator RewindTimer(float seconds)
		{			
			yield return new WaitForSeconds(seconds);
            StopRewind();
		}

        
    }

    public class RewindDataManager
    {
        private List<RecordedData> _recordedData;
        private RecordedData[] _recordedDataArray;
        private RecordedData[] _bufferNewData;
        private bool bufferUsed = false;
        private int arrayIndex = 0;
        private int _maxSize;

        private bool _useArray;
        
        public RewindDataManager(bool useArray, int maxSize)
        {
            _maxSize = maxSize;
            this._useArray = useArray;
            this._recordedData = new List<RecordedData>();
            this._recordedDataArray = new RecordedData[maxSize];
            this._bufferNewData = new RecordedData[maxSize];
        }

        public void Enqueue(RecordedData data)
        {
            if(!_useArray)
            {
                _recordedData.Add(data);
                Debug.Log(_recordedData[_recordedData.Count -1].pos);
            }
            else
            {
                

                if(arrayIndex >= (2*_maxSize-1))
                {
                    _recordedDataArray = (RecordedData[]) _bufferNewData.Clone();
                    Array.Clear(_bufferNewData, 0, _bufferNewData.Length);
                    arrayIndex = _maxSize-1;
                }

                if(arrayIndex <= _maxSize-1)
                {
                    _recordedDataArray[arrayIndex] = data;
                }
                else
                {
                    int index = arrayIndex % _maxSize;
                    _bufferNewData[index] = data;
                }
                arrayIndex++;
            }
        }

        public void Dequeue()
        {
            if(!_useArray)
            {
                _recordedData.RemoveAt(0);
            }

        }

        public RecordedData Get(int index)
        {
            if(!_useArray)
            {
                return _recordedData[index];
            }
            else
            {
                
                if(arrayIndex >= _maxSize)
                {
                    index = arrayIndex % _maxSize;
                    arrayIndex--;
                    if(arrayIndex < 0)
                        arrayIndex = 0;
                    return _bufferNewData[index];

                }
                
                if(arrayIndex <= _maxSize-1)
                {
                    arrayIndex--;
                    if(arrayIndex < 0)
                        arrayIndex = 0;
                    return _recordedDataArray[arrayIndex];
                } 
                              
                return new RecordedData();
            }
        }

        public int Size()
        {
            if(!_useArray)
            {
                return _recordedData.Count;
            }
            else
            {
                return _recordedDataArray.Length;
            }
        }

        public void Clear()
        {
            if(!_useArray)
            {
                _recordedData.Clear();
            }
            else
            {
                Array.Clear(_recordedDataArray, 0, _recordedDataArray.Length);
                arrayIndex = 0;
            }
        }
    }
}


