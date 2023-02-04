using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables{

    [CreateAssetMenu(menuName = "VariableSO/Bool Variable")]
    public class BoolVariableSO : ScriptableObject
    {
        public bool Value;
    }
}