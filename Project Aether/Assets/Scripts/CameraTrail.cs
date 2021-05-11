using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrail : MonoBehaviour
{
    public Transform target;
    public float trailDistance =-2f;
    public float heightOffset=-10f;
    public float delay=1f;
   
    void Update()
    {
        Vector3 followPos = target.position -target.forward*trailDistance;
        followPos.y+=heightOffset;
        transform.position+=(followPos-transform.position)*delay;
        transform.LookAt(target.transform);
    }
}
