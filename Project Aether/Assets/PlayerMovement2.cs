using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
[RequireComponent(typeof(InputManager))]
public class PlayerMovement2 : MonoBehaviour
{
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheel;
    public InputManager im;
    public float strenghtCoefficient =20000f;
    public float maxTurn=20f;
    
    
    // Start is called before the first frame update
    void Start()
    {
       im=GetComponent<InputManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       foreach (WheelCollider wheel in throttleWheels)
       {
           wheel.motorTorque=strenghtCoefficient*Time.deltaTime*im.throttle;
       }
       foreach (WheelCollider wheel in steeringWheel)
       {
           wheel.steerAngle=maxTurn*im.steer;
       }
        
       
    }
}