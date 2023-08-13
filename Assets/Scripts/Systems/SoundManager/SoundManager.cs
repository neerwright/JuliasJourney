using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    
    
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _playerAudioSource, _effectsSource, _musicSource, _ambienceSource;
        // Start is called before the first frame update
        void Awake()
        {
            _musicSource.loop = true;
            _ambienceSource.loop = true;
        }

        public void PlaySoundPlayer(AudioClip clip, float volume)
        {
            _playerAudioSource.volume = volume;
            _playerAudioSource.PlayOneShot(clip);
        }

        public void PlaySound(AudioClip clip, float volume)
        {
            _effectsSource.volume = volume;
            _effectsSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip, float volume)
        {
            Debug.Log("PlayMusic");
            Debug.Log(clip);
            _musicSource.volume = volume;
            if(_musicSource.isPlaying)
                _musicSource.Pause();
            _musicSource.clip = clip;
            
            _musicSource.Play();
        }

        public void StopMusic()
        {
            _musicSource.Pause();
        }

        public void PlayAmbience(AudioClip clip, float volume)
        {
            
            _ambienceSource.volume = volume;
            if(_ambienceSource.isPlaying)
                _ambienceSource.Pause();
            _ambienceSource.clip = clip;
            
            _ambienceSource.Play();
        }

        public void StopAmbience()
        {
            _ambienceSource.Pause();
        }

        public void ChangeMasterVolume(float volume)
        {
            AudioListener.volume = volume;
        }
    }
}

