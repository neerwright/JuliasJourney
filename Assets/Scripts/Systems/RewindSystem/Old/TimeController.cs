using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace RewindSystem
{

    public class TimeController : MonoBehaviour
    {
        

        [SerializeField] IntGameEvent startRewinding;
        [SerializeField] GameObjectStringSet timeObjects;
        [SerializeField] FloatVariableSO PlayerVelocity;
        
        public bool Rewind {get;set;}
        private bool _stepedBack = false;

        private RecordedData[,] _recordedData;
        private const int recordMax = 300;
        private int recordCount;
        private int recordIndex;

        private Dictionary<string, int> timeObjectsIndexDict ;

        

        public RecordedData getRecordedData(string ObjectName, int recordIndex)
        {
            int objectIndex = timeObjectsIndexDict[ObjectName];
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
                if(!_stepedBack)
                {
                    _stepedBack = true;
                    startRewinding?.Raise(recordIndex - 1);    
                }
                
            }
            else
            {
                if(_stepedBack)
                {
                    _stepedBack = false;
                    recordCount = 1;
                    
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
                if(recordCount >= recordMax)
                {
                    recordCount = 1;
                }
                recordIndex = recordCount;
                
            }
        }
    }
}
