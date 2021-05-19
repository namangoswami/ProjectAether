using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrail : MonoBehaviour
{
    public Transform target;
    public List<Transform> targets;
    public bool switchCam=false;
    private int choice=1;
    public float trailDistance =-2f;
    public float heightOffset=-10f;
    public float delay=1f;
    public void enableCamSwitch() {
       switchCam=true;
    }
    void Update()
    {
        
        Debug.Log(switchCam);
        if(switchCam)
        {
            if(choice==targets.Count)
            choice=0;
            else
            {
                choice++;
                
            }
        }
        switchCam=false;
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(choice==targets.Count)
            choice=0;
            else
            {
                choice++;
                
            }
        }
        target=targets[choice-1];
        Vector3 followPos = target.position -target.forward*trailDistance*delay;
        followPos.y+=heightOffset;
        transform.position+=(followPos-transform.position);
        transform.LookAt(target.transform);
    }
}
