using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables{

    [CreateAssetMenu(menuName = "VariableSO/Float Variable")]
    public class FloatVariableSO : ScriptableObject
    {
        public float Value;
    }
}