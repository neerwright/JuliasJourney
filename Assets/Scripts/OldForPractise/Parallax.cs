using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace environment
{
    public class Parallax : MonoBehaviour
{
    //[SerializeField]
    Camera cam;
    //[SerializeField]
    Transform Player;

    Vector2 startPosition;
    float startZ;
    float distanceFromSubject => transform.position.z - Player.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0? cam.farClipPlane: cam.nearClipPlane));

    Vector2 newPos;
    Vector2 travel => (Vector2)cam.transform.position - startPosition;
    float parallaxVector => Mathf.Abs(distanceFromSubject / clippingPlane);
    
    
    public void Start()
    {
        cam = Camera.main;
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    public void Update() {
        newPos = startPosition + travel* parallaxVector;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}
}

