using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

public class WriteNewEnvironmentParentObject : MonoBehaviour
{
    [SerializeField] private GameObjectVariableSO _currentEnvironmentParentObjectSO;
    [SerializeField] private GameObject _currentEnvironmentParentObject;
    // Start is called before the first frame update
    void Awake()
    {
        _currentEnvironmentParentObjectSO.GObject = _currentEnvironmentParentObject;
    }
}
