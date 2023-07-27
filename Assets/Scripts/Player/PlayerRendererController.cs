using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    

    public class PlayerRendererController : MonoBehaviour
    {
        [SerializeField] private GameObject _playerModel;
        // Start is called before the first frame update
        void Start()
        {
            //turn off
            _playerModel.SetActive(false);
        }

        public void TurnRenderesOn()
        {
            _playerModel.SetActive(true);
        }

        
    }
}
