using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class AddCamTargetTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D Collider)
        {
            if(Collider.gameObject.tag == "Player")
            {
                //ProCamera2D.Instance.AddCameraTarget(transform, 0.1f, 0.1f, 0f, Vector2.zero);
            
            }
        }

        private void OnTriggerExit2D(Collider2D Collider)
        {
            if(Collider.gameObject.tag == "Player")
            {
                //ProCamera2D.Instance.RemoveCameraTarget(transform);
            
            }
        }
}
