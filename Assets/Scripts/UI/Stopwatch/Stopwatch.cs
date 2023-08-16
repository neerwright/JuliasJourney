using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System; 
using System.Collections.Specialized;

namespace UI 
{
    public class Stopwatch : MonoBehaviour
    {
        private float _currentTime = 0f; //seconds
        private float _totalTime = 0f;
        private Dictionary<string, float> _timeSegments;
        private Queue<string> _last3Islands;
        private List<TextMeshProUGUI> _islandTexts;
        private string _currentIsland;
        private bool _timerActive;

        [SerializeField] private TextMeshProUGUI _stageText;
        [SerializeField] private TextMeshProUGUI _timer1Text;
        [SerializeField] private TextMeshProUGUI _timer2Text;
        [SerializeField] private TextMeshProUGUI _timer3Text;
        [SerializeField] private TextMeshProUGUI _totalText;

        [SerializeField] private GameObject _stopWatch;
        private bool _active = false;

        public void ResetTimer()
        {
            _currentTime = 0f;
            _timeSegments[_currentIsland] = 0f;
        }

        public void OnToggleTimer()
        {
            if(_active)
            {
                _stopWatch.SetActive(false);
            }
            else
            {
                _stopWatch.SetActive(true);
            }
            _active = !_active;
        }

        public void StartTimer(string islandName)
        {
            StopTimer();
            _currentTime = 0f;
            AddNewSegment(islandName);
            _timerActive = true;
            _currentIsland = islandName;
            _last3Islands.Enqueue(islandName);
            if(_last3Islands.Count > 3)
            {
                _last3Islands.Dequeue();
            }
        }

        public void StopTimer()
        {
            _timerActive = false;
            
        }

        private void AddNewSegment(string islandName)
        {
            if(_timeSegments != null)
            {
                if(!_timeSegments.ContainsKey(islandName))
                {
                    _timeSegments.Add(islandName, 0f);
                }
                else
                {
                    Debug.Log("timer already in dict");
                }
            }
            else
            {
                Debug.Log("dict missing");
            }




        }

        private void Awake()
        {
            _timeSegments = new Dictionary<string, float>();
            _last3Islands = new Queue<string>();
            _islandTexts = new List<TextMeshProUGUI>();
            
            
            

        }

        void Start()
        {
            _islandTexts.Add(_timer1Text);
            _islandTexts.Add(_timer2Text);
            _islandTexts.Add(_timer3Text);
            _stopWatch.SetActive(false);
        }

        private void WriteText()
        {
            int index = 0;
            foreach (string segment in _last3Islands)
            {
                TimeSpan time = TimeSpan.FromSeconds(_timeSegments[segment]);
                string segmentText = $"{segment}: {time.Minutes.ToString()} : {time.Seconds.ToString()}" ;
                _islandTexts[index].SetText(segmentText);
                index++;
                if(index >= _islandTexts.Count)
                    break;
            }

        }

        // Update is called once per frame
        void Update()
        {
            if(_timerActive)
            {
                _currentTime += Time.deltaTime;
                _totalTime += Time.deltaTime;
                _timeSegments[_currentIsland] = _currentTime;
                WriteText();
                
            }
            
        }

    }
}

