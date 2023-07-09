using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Player;
using GameManager;
using Com.LuisPedroFonseca.ProCamera2D;

namespace environment 
{
    
    public class ZoomOutToWaterTrigger : MonoBehaviour
    {
        [SerializeField] private float _delay = 1f;
        [SerializeField] private Transform _zoomTarget;
        private bool _active = false;
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.tag == "Player" && !_active)
            {  
                _active = true;
                ProCamera2D.Instance.AddCameraTarget(_zoomTarget, 0.6f, 0.6f, 0f, Vector2.zero);
                StartCoroutine("RemoveCamTarget");
            }
        }


    

        private IEnumerator RemoveCamTarget()
        {
            yield return new WaitForSeconds(_delay);
            ProCamera2D.Instance.RemoveCameraTarget(_zoomTarget);
            _active = false;

        }
    }
}