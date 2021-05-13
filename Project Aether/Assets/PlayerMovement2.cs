using System.Collections;
using System.Collections.Generic;


using UnityEngine;
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(LightingManager))]
public class PlayerMovement2 : MonoBehaviour
{
    public List<WheelCollider> throttleWheels;
    public List<GameObject> steeringWheel;
    public List<GameObject> MeshesL, MeshesR;
    public GameObject wheelL, wheelR;
    public InputManager im;
    public LightingManager lm;
    public float strenghtCoefficient =20000f;
    public float maxTurn=20f;
    public Transform CM;
    public Rigidbody rb;
    public float brakeStrength;
    
    // Start is called before the first frame update
    void Start()
    {
       im=GetComponent<InputManager>();
       rb=GetComponent<Rigidbody>();
       if(CM)
       {
           rb.centerOfMass=CM.position;
       }
    }

    void Update()
    {
        if (im.l)
        {
            lm.ToggleHeadlights();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       foreach (WheelCollider wheel in throttleWheels)
       {
          
            if(im.brake)
            {   
                wheel.motorTorque=0f;
                wheel.brakeTorque=brakeStrength*Time.deltaTime;

            }
            else
            {
                 wheel.motorTorque=strenghtCoefficient*Time.deltaTime*im.throttle;
                wheel.brakeTorque= 0f;
            }
       
       }
       foreach (GameObject wheel in steeringWheel)
       {
           wheel.GetComponent<WheelCollider>().steerAngle=maxTurn*im.steer;
            wheelL.transform.localEulerAngles=new Vector3(0f, im.steer*maxTurn+180, 0f);
            wheelR.transform.localEulerAngles=new Vector3(0f, im.steer*maxTurn, 0f);
           
         }
        
       foreach(GameObject mesh in MeshesR)
       {
           mesh.transform.Rotate(rb.velocity.magnitude*(transform.InverseTransformDirection(rb.velocity).z >=0?1:(-1))/(2*Mathf.PI*0.15f), 0f, 0f);
       }
        foreach(GameObject mesh in MeshesL)
       {mesh.transform.Rotate(rb.velocity.magnitude*(transform.InverseTransformDirection(rb.velocity).z >=0?(1):(-1))/(-2*Mathf.PI*0.15f), 0f, 0f);
       }
    }
}