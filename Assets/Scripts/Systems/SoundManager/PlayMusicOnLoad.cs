using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace Sounds
{
    public class PlayMusicOnLoad : MonoBehaviour
    {
        [SerializeField] private GameEvent _playNextSongPart;
        [SerializeField] private bool  _playOnAwake;

        private bool _triggered = false;

        private void Start()
        {
            if(_playOnAwake)
                _playNextSongPart.Raise();
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(_triggered)
                return;

            if(col.gameObject.tag == "Player")
            {
                _triggered = true;
                _playNextSongPart.Raise();
            }
            
        }

    }
}

