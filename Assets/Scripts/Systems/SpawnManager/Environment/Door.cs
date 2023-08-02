using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sounds;

namespace environment
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject _closedDoor;
        [SerializeField] private GameObject _openDoor;
        // Audio
        [SerializeField] private AudioClipGameEvent _audioClipGameEvent = default;
        [SerializeField] private AudioClip _audioClip;

        [Range(0,1)]
        [SerializeField] float _volume;

        private bool _triggered = false;
        void Start()
        {
            _openDoor.SetActive(false);
            _closedDoor.SetActive(true);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.tag == "Player" && !_triggered)
            {
                _openDoor.SetActive(true);
                _closedDoor.SetActive(false);
                _triggered = true;
                _audioClipGameEvent.Raise(_audioClip, _volume);
            }
        }
    }
}
