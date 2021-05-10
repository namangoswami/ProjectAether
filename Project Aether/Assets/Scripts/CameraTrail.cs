using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrail : MonoBehaviour
{
    public Transform target;
    public float trailDistance =0f;
    public float heightOffset=0f;
   
    void Update()
    {
        Vector3 followPos = target.position -target.forward*trailDistance;
        followPos.y+=heightOffset;
        transform.position+=(followPos-transform.position);
        transform.LookAt(target.transform);
    }
}
