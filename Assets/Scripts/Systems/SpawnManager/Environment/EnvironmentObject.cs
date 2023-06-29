using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
using environment;

public class EnvironmentObject : MonoBehaviour
{
    public Vector2 locationData;
    public StringVariableSO environmentType;
    public GameObject prefab;

    private void OnValidate()
    {
        locationData = gameObject.transform.position;
    }

}
