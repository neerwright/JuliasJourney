using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animancer.Assets.Scripts.Systems.RewindSystem
{

    public class PlayerRewindData : MonoBehaviour , IRewindData
    {

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void Rewind(RecordedData data)
        {
            gameObject.transform.position = data.pos;
        }
    }
}