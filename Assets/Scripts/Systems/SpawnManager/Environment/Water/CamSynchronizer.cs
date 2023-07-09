using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Player;
using GameManager;
using Com.LuisPedroFonseca.ProCamera2D;

namespace environment 
{
    public class CamSynchronizer : MonoBehaviour
    {
        [SerializeField] private Camera _noPostPlayerCam;

        private float _size;
        private bool _isZoomingOut;
        private Camera _mainCam;
        
        private void Update()
        {

            if(_mainCam != null)
            {
                _size = _mainCam.orthographicSize;
                _noPostPlayerCam.orthographicSize = _size;
            }
                
        }
        

        private void Awake()
        {
                _mainCam = Camera.main;
        }
    }


}
