using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class PlayMusicOnLoad : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [Range(0,1)]
        [SerializeField] private float _volume = 1;
        [SerializeField] private AudioClipGameEvent _playMusicEvent;

        void Start()
        {
            _playMusicEvent.Raise(_clip, _volume);
        }

    }
}

