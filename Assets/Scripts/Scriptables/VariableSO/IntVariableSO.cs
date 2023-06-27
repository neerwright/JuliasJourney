using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables{

    [CreateAssetMenu(menuName = "VariableSO/Int Variable")]
    public class IntVariableSO : ScriptableObject
    {
        public int Value;
    }
}