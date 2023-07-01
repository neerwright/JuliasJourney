using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    
    
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _playerAudioSource, _effectsSource, _musicSource;
        // Start is called before the first frame update
        void Start()
        {
            _musicSource.loop = true;
        }

        public void PlaySoundPlayer(AudioClip clip)
        {
            _playerAudioSource.PlayOneShot(clip);
        }

        public void PlaySound(AudioClip clip)
        {
            _effectsSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip)
        {
            if(_musicSource.isPlaying)
                _musicSource.Pause();
            _musicSource.clip = clip;
            
            _musicSource.Play();
        }

        public void ChangeMasterVolume(float volume)
        {
            AudioListener.volume = volume;
        }
    }
}

