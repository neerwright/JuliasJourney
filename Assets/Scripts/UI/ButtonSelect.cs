using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI 
{
    public class ButtonSelect : MonoBehaviour
    {
        public Button primaryButton;
        
        void Start()
        {
            primaryButton.Select();
        }

    }
}

