using UnityEngine;

namespace Scriptables{

    [CreateAssetMenu(menuName = "VariableSO/Vector2 Variable")]
    public class Vector2VariableSO : ScriptableObject
    {
        public Vector2 Value;
    }
}