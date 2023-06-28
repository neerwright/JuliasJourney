using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables{

    [CreateAssetMenu(menuName = "DataSO/Animation Data")]
    public class AnimationTrackerSO : ScriptableObject
    {
        public double Time;
        public AnimationClip Clip;
    }
}
