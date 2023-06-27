using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace RewindSystem
{

    public class TimeController : MonoBehaviour
    {
        public struct RecordedData
        {
            public Vector2 pos;
            public float vel;
        }

        [SerializeField] IntGameEvent startRewinding;
        [SerializeField] GameEvent stopRewinding;
        [SerializeField] GameObjectStringSet timeObjects;
        [SerializeField] FloatVariableSO PlayerVelocity;
        
        public bool Rewind {get;set;}
        //private bool _stepedBack = false;

        private RecordedData[,] _recordedData;
        private const int recordMax = 10000;
        private int recordCount;
        private int recordIndex;

        private Dictionary<string, int> timeObjectsIndexDict ;

        private bool raisedEvent = false;

        public RecordedData getRecordedData(string ObjectName, int recordIndex)
        {
            int objectIndex = timeObjectsIndexDict[ObjectName];
            Debug.Log(_recordedData[objectIndex, recordIndex].pos);
            return _recordedData[objectIndex, recordIndex];
        }

        private void Awake()
        {
            timeObjectsIndexDict = new Dictionary<string, int>();
            _recordedData = new RecordedData[timeObjects.Size(), recordMax];
        }


        void Update()
        {
            if(Rewind)
            {
                if(!raisedEvent)
                {
                    raisedEvent = true;
                    startRewinding?.Raise(recordIndex - 1);    
                }
                
            }
            else
            {
                
                if(raisedEvent)
                {
                    raisedEvent = false;
                    stopRewinding?.Raise();
                }

                //capture data
                for(int objectIndex = 0; objectIndex < timeObjects.Size(); objectIndex++)
                {
                    GameObject timeObject = timeObjects.Items[objectIndex].ItemOne;
                    
                    RecordedData data = new RecordedData();
                    data.pos = timeObject.transform.position;
                    data.vel = PlayerVelocity.Value;
                    _recordedData[objectIndex, recordCount] = data;
                    //Debug.Log(_recordedData[objectIndex, recordCount].pos);
                    timeObjectsIndexDict[timeObjects.Items[objectIndex].ItemTwo] = objectIndex;
                    Debug.Log(objectIndex);
                }
                recordCount++;
                recordIndex = recordCount;
                
            }
        }
    }
}
