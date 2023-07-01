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

        public void PlaySoundPlayer(AudioClip clip, float volume)
        {
            _playerAudioSource.volume = volume;
            _playerAudioSource.PlayOneShot(clip);
        }

        public void PlaySound(AudioClip clip, float volume)
        {
            _playerAudioSource.volume = volume;
            _effectsSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip, float volume)
        {
            _playerAudioSource.volume = volume;
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

