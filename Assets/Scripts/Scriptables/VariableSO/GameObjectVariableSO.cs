using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VariableSO{

    [CreateAssetMenu(menuName = "VariableSO/GameObject Variable")]
    public class GameObjectVariableSO : ScriptableObject
    {
        public GameObject gameObject;
    }
}