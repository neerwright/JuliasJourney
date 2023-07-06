using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
namespace Sounds
{
    public class PlayAmbienceTrigger : MonoBehaviour
    {
            [SerializeField] GameEvent _stopMusicEvent;
            [SerializeField] AudioClipGameEvent _playAmbienceEvent;
            [SerializeField] private AudioClip _clip;
            [SerializeField] private float _volume = 1f;

            [SerializeField] private bool _stopAmbienceSounds = false;
            [SerializeField] GameEvent _stopAmbienceEvent;

            private bool _triggered = false;

            private void OnTriggerEnter2D(Collider2D Collider)
            {
                if(Collider.gameObject.tag == "Player")
                {
                    if(_stopAmbienceSounds)
                    {
                        _stopAmbienceEvent.Raise();
                    }
                    else
                    {
                        if(!_triggered)
                        {
                            Debug.Log("Raise");
                            _stopMusicEvent?.Raise();
                            _playAmbienceEvent?.Raise(_clip, _volume);
                            _triggered = true;
                        }
                    }
                    


                    
                }

            }
    }
}

