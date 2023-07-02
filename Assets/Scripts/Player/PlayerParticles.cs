using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class PlayerParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem TrailsPS;
        [SerializeField] private ParticleSystem DustPS;

        private PlayerScript _playerScript;
        private const float THRESHOLD = 15f;

        void Awake()
        {
            _playerScript = GetComponent<PlayerScript>();
        }

        public void PlayTrailsParticle()
        {
            TrailsPS.Play();
        }

        public void PlayDustParticle()
        {
            DustPS.Play();
        }

        void Update()
        {
            if(DustPS.isPlaying)
            {
                if(_playerScript.movementVector.x > THRESHOLD)
                    DustPS.Stop();
            }
        }


    }
}


