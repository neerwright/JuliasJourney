using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace Sounds
{
    public enum MusicState
    { 
        Intro = 0,
        Kick = 1,
        Shaker = 2,
        Bass = 3,
        PianoTransition = 4,
        MainTrumpet = 5,
        SaxIntro = 6,
        SaxTransition = 7,
        SaxPiano = 8,
        Drums = 9,
        MainLoop = 10,
        SadTransition = 11,
        SadSax = 12,
        End = 13,
        FinalTransition = 14,
        Finale = 15,
        notPlaying = 16
    }

    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _musicClips;
        [SerializeField] private AudioClip[] _altMusicClips;
        //[SerializeField] private float[] _musicClipLengths;
        [SerializeField] private float _volume = 1;
        [SerializeField] private AudioClipGameEvent _playMusicEvent;
        [SerializeField] private GameEvent _stopMusicEvent;


        private MusicState _state;
        private int _nextPartCounter = 0;
        private float _time;
        private bool _altToggledOn = false;
        private int _saxAltNumber = 3;
        private bool _silenceAfterPlaying = false;

        [SerializeField] private bool _startPlaying = false;

        public void PlayNextPart()
        {
            if(_nextPartCounter > 0)
            {
                Debug.Log("COROUTINE");
                StartCoroutine("WaitUntilLastTransitionFinished");
            }

            switch(_state) 
            {
                case MusicState.End:
                    _nextPartCounter = 1;
                    _silenceAfterPlaying = true;                   
                    break;

                case MusicState.SaxTransition:
                    _nextPartCounter = 1;
                    _silenceAfterPlaying = true;
                    break;

                case MusicState.SaxPiano:
                    _nextPartCounter = 3;
                    _state = MusicState.Drums;
                    _playMusicEvent.Raise(_musicClips[(int) _state], _volume);
                    _silenceAfterPlaying = false;
                    _time = 0f;

                    break;

                case MusicState.Bass:
                    _nextPartCounter = 1;
                    _silenceAfterPlaying = true;
                    
                    break;
                case MusicState.PianoTransition:
                    _nextPartCounter = 1;
                    _state = MusicState.MainTrumpet;
                    _playMusicEvent.Raise(_musicClips[(int) _state], _volume);
                    _silenceAfterPlaying = false;
                    _time = 0f;

                    break;

                case MusicState.SadSax:
                    _nextPartCounter = 2;

                    break;


                case MusicState.notPlaying:
                    _state = MusicState.Intro;
                    _playMusicEvent.Raise(_musicClips[(int) _state], _volume);
                    _time = 0f;
                    Debug.Log("intro");
                    break;
                default:
                    _nextPartCounter = 1;
                    
                    break;
            }

            
            
        }


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
            float trackLength = _musicClips[(int) _state].length;
            if(_altToggledOn)
            {
                switch(_state) 
                {
                    case MusicState.Intro:
                        trackLength = _altMusicClips[0].length;
                        break;


                    case MusicState.Bass:
                        trackLength = _altMusicClips[1].length;
                        break;



                    case MusicState.SaxIntro:
                        trackLength = _altMusicClips[2].length;
                        break;


                    case MusicState.SadSax:
                        trackLength = _altMusicClips[_saxAltNumber].length;
                        break;
                    default:               
                        break;
                }
            }


            if(_time  >= trackLength)
            {
                Debug.Log("TIMER OVEEEER");
                Debug.Log(_nextPartCounter);

                if(_nextPartCounter > 0)
                {
                    //_stopMusicEvent.Raise();
                    _nextPartCounter--;
                    int stateNumber = (int) _state;
                    stateNumber++;
                    if(stateNumber >= _musicClips.Length)
                        stateNumber = _musicClips.Length - 1;
                    _state = (MusicState)stateNumber;
                    
                    Debug.Log(_silenceAfterPlaying);
                    if(_silenceAfterPlaying)
                        _playMusicEvent.Raise(_musicClips[(int) _state], 0);
                    else
                        _playMusicEvent.Raise(_musicClips[(int) _state], _volume);


                    Debug.Log("PLAYYYYYYYYYYYYYY NEXT");
                    Debug.Log(_musicClips[(int) _state]);

                    
                }

                //loop current clip, reset time
                _time = 0f;
                Debug.Log("Reset");
                if(!_silenceAfterPlaying)
                    SwitchAlternatives();

            }


        }

        private void SwitchAlternatives()
        {

                switch(_state) 
                {
                    case MusicState.Intro:

                        if(_altToggledOn)
                        {
                            _playMusicEvent.Raise(_musicClips[(int) _state], _volume);
                        }
                        else
                        {
                            _playMusicEvent.Raise(_altMusicClips[0], _volume);
                        }
                        break;


                    case MusicState.Bass:

                        if(_altToggledOn)
                        {
                            _playMusicEvent.Raise(_musicClips[(int) _state], _volume);
                        }
                        else
                        {
                            _playMusicEvent.Raise(_altMusicClips[1], _volume);
                        }
                        break;



                    case MusicState.SaxIntro:

                        if(_altToggledOn)
                        {
                            _playMusicEvent.Raise(_musicClips[(int) _state], _volume);
                        }
                        else
                        {
                            _playMusicEvent.Raise(_altMusicClips[2], _volume);
                        }
                        break;


                    case MusicState.SadSax:
                        Debug.Log("SadSax alt");
                        if(_altToggledOn)
                        {
                            Debug.Log("state");
                            Debug.Log(_state);
                            _playMusicEvent.Raise(_musicClips[12], _volume);
                        }
                        else
                        {
                            Random.seed = System.DateTime.Now.Millisecond;
                            _saxAltNumber = Random.Range(3, 5);
                            Debug.Log("SadSax altttttt");
                            Debug.Log(_saxAltNumber);
                            _playMusicEvent.Raise(_altMusicClips[_saxAltNumber], _volume);
                        }
                           
                        break;



                    default:               
                        break;
                }
            

            _altToggledOn = !_altToggledOn;
            
        }

        
        private IEnumerator WaitUntilLastTransitionFinished()
        {
            while(_nextPartCounter > 0)
            {
                yield return new WaitForSeconds(0.5f);
            }
            PlayNextPart();
            
        }


    }
}