using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineChildrenMesh : MonoBehaviour
{
    private MeshFilter[] _meshfilters;
    

    void Awake()
    {
        _meshfilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combinedMeshes = new CombineInstance[_meshfilters.Length - 1];

        int i = 1;
        while(i < _meshfilters.Length)
        {
            combinedMeshes[i-1].mesh = _meshfilters[i].sharedMesh;
            combinedMeshes[i-1].transform = _meshfilters[i].transform.localToWorldMatrix;
            i++;
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combinedMeshes);
    }


}
