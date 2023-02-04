using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables{

    [CreateAssetMenu(menuName = "VariableSO/String Variable")]
    public class StringVariableSO : ScriptableObject
    {
        public string Value;
    }
}
