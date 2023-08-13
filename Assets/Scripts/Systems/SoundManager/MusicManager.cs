using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace Sounds
{
    public enum MusicState
    {
        
        IntroApreggio = 0,
        DrumIntro = 1,
        DrumLoop = 2,
        FirstTrumpetPart = 3,
        PianoLoop = 4,
        SecondTrumpetSaxPart = 5,
        notPlaying = 10
    }

    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _musicClips;
        //[SerializeField] private float[] _musicClipLengths;
        [SerializeField] private float _volume = 1;
        [SerializeField] private AudioClipGameEvent _playMusicEvent;
        [SerializeField] private GameEvent _stopMusicEvent;


        private MusicState _state;
        private int _nextPartCounter = 0;
        private float _time;

        [SerializeField] private bool _startPlaying = false;

        void Start()
        {
            
            _state = MusicState.notPlaying;
             _nextPartCounter = 0;
            
            
        }

        void Update()
        {
            if(_state == MusicState.notPlaying)
                return;
            
            _time += Time.deltaTime;

            //at end of current clip, start the neext one
            if(_time  >= _musicClips[(int) _state].length)
            {
                if(_nextPartCounter > 0)
                {
                    _stopMusicEvent.Raise();
                    Debug.Log("_jumpCounter");
                    Debug.Log(_nextPartCounter);
                    _nextPartCounter--;
                    int stateNumber = (int) _state;
                    stateNumber++;
                    if(stateNumber >= _musicClips.Length)
                        stateNumber = _musicClips.Length - 1;
                    _state = (MusicState)stateNumber;;

                    _playMusicEvent.Raise(_musicClips[(int) _state], _volume);
                    Debug.Log("PLAYYYYYYYYYYYYYY NEXT");
                    Debug.Log(_musicClips[(int) _state]);
                }

                //loop current clip, reset time
                _time = 0f;
                Debug.Log("Reset");

            }

            

            
            //if(_time >= _musicClips[_stateNumber].length )
            //{
            //    
            //    _time = 0f;
            //    _stopMusicEvent.Raise();
            //    _playMusicEvent.Raise(_musicClips[_stateNumber], _volume);
            //}
        }

        public void PlayNextPart()
        {

            switch(_state) 
            {
                case MusicState.IntroApreggio:
                    _nextPartCounter = 2;
                    break;
                case MusicState.notPlaying:
                    _state = MusicState.IntroApreggio;
                    _playMusicEvent.Raise(_musicClips[(int) _state], _volume);
                    _time = 0f;
                    Debug.Log("intro");
                    break;
                default:
                    _nextPartCounter = 1;
                    
                    break;
            }

            
            
        }


    }
}