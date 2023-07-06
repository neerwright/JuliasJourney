using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace RewindSystem
{

    public class BoxRewind  : MonoBehaviour , IRewindData
    {
        public void Rewind(RecordedData data)
        {
            gameObject.transform.position = data.pos;
        }
    }
}
