using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform path;
    public float MaxSteerAngle=45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public GameObject wheelL, wheelR;
    public Rigidbody rb;
    public bool aiFollowChoice=false;
    public List<GameObject> MeshesL, MeshesR;
    public float maxMotorTorque = 80f;
    public float maxBrakeTorque=150f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public bool isBrakingDistance=false;
    public bool isBraking = false;
    private bool avoiding=false;
    private float targetSteerAngle=0;
    private float turnSpeed=5f;
    private float reversingMultiplier=1;
    private float motorTorqueMultiplier=1;
    private float motorTorqueMultiplier2=1;
    private float reversingMultiplier2=1;
    private bool isReversing=false, isReversingCollision=false, reverseCar=false;
    private bool hitFlag=false;
    [Header("Senseors")]
    public float sensorLength = 5f;
    public Vector3 frontSensorPosition=new Vector3(0, 0.2f, 0.5f);
    public float frontSideSensorPosition=0.2f;
    public float frontSensorAngle=30;


    private List<Transform> nodes;
    private int currentNode = 0;


    // Start is called before the first frame update
    private void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {

                nodes.Add(pathTransforms[i]);

            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {   
        if(!aiFollowChoice)
        checkReverse();
        Sensors();
        
        Reversing();
        ApplySteer();
        Drive();
        CheckWaypointDistance();
        Braking();
        lerpTurn();
      //  Debug.Log(Vector3.Dot(transform.forward, nodes[currentNode].position-transform.position));
        Debug.DrawLine(transform.position, nodes[currentNode].position);
       // Debug.Log(hitFlag);
        //Debug.Log(Vector3.Dot(transform.forward, nodes[currentNode].forward));
    }
    
    private void checkReverse()
    {
        if(Vector3.Dot(transform.forward, nodes[currentNode].position-transform.position)<-10)
        {
            isReversing=true;
            motorTorqueMultiplier2=-1;
           // reversingMultiplier2=-1;
        }
       
        else
        {
            isReversing=false;
            motorTorqueMultiplier2=1;
           // reversingMultiplier2=1;
        }
    }
    private void Sensors()
    {
        hitFlag=false;
        RaycastHit hit;
        Vector3 sensorStartPos=transform.position;
        sensorStartPos+=transform.forward*frontSensorPosition.z;
        sensorStartPos+=transform.up*frontSensorPosition.y;
        float avoidMultiplier=0;
        avoiding=false;
        //front center shorter sensor
        //for(float Vector3 iv=sensorStartPos, float y=-0.01f;;)
        if(Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
           
             Debug.DrawLine(sensorStartPos, hit.point);
             
            avoiding=true;
            isBrakingDistance=true;
            isReversingCollision=true;
        }
        else
        {
            isBrakingDistance=false;
            isReversingCollision=false;
        }
       
        
       //front right sensor
        sensorStartPos+=transform.right*frontSideSensorPosition;
        
        Vector3 iv=transform.position;
        Vector3 iv2=transform.position;
        
        iv+=transform.forward*(frontSensorPosition.z);
        iv+=transform.up*frontSensorPosition.y;
        iv+=transform.right*(frontSideSensorPosition-0.25f);
      iv2+=transform.forward*frontSensorPosition.z;
        iv2+=transform.up*frontSensorPosition.y;
        iv2+=transform.right*frontSideSensorPosition;
    
       // Debug.Log(iv);

        float y=-0.01f;
        for(  int k=0;k<150;iv+=transform.right*y, k++)
        {
          //  Debug.DrawLine(iv, transform.forward);
            //Debug.Log(k);
            if(Physics.Raycast(iv, transform.forward, out hit, 1f))
        {
         //  Debug.Log("Hitit");
             Debug.DrawLine(iv, hit.point);
             hitFlag=true;
        }
        if(hitFlag==true)   {
            avoiding=true;
            isBrakingDistance=true;
            isReversingCollision=true;
            
        }
        else
        {
            avoiding=false;
            isBrakingDistance=false;
            isReversingCollision=false;
        }}
        
        /*float j=frontSideSensorPosition;
        for(Vector3 i=sensorStartPos;i.right<-2*transform.right*frontSideSensorPosition;i+=transform.right*j)
        {
             j-=0.01f;
            if(Physics.Raycast(i, transform.forward, out hit, sensorLength+1f))
        {
                 Debug.DrawLine(sensorStartPos, hit.point);
                  avoiding=true;
                 if(j>0)
                 avoidMultiplier-=1f;
                 else if(j<0)
                avoidMultiplier+=1f;
        }
        }*/
        if(Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength+1f))
        {
        Debug.DrawLine(sensorStartPos, hit.point);
         avoiding=true;
         avoidMultiplier-=1f;
        }
         
        
        //front right angled sensor
        else if(Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up)*transform.forward, out hit, sensorLength))
        {
        Debug.DrawLine(sensorStartPos, hit.point);
         avoiding=true;
         
         avoidMultiplier-=0.5f;
        }
        
        //front left sensor
        sensorStartPos-=2*transform.right*frontSideSensorPosition;
        if(Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength+1f))
        {
        Debug.DrawLine(sensorStartPos, hit.point);
         avoiding=true;
         avoidMultiplier+=1f;
        }
        else
        {
            isBraking=false;
        }
        //front left angled sensor
         if(Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up)*transform.forward, out hit, sensorLength))
        {
        Debug.DrawLine(sensorStartPos, hit.point);
         avoiding=true;
         avoidMultiplier+=0.5f;
        }
        //front center longer sensor
        if(avoidMultiplier==0)
          if(Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength+2f))
        {   
            isBraking=true;
           
             Debug.DrawLine(sensorStartPos, hit.point);
            avoiding=true;
            for(float i=0;i<=(80);i++)
            {
                    if(Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-i, transform.up)*transform.forward, out hit, sensorLength+5f))
                 {
                 Debug.DrawLine(sensorStartPos, hit.point);
         
                }
                else
                {
                    Debug.Log("Left");
                    avoidMultiplier=-1.3f;
                    isBraking=false;
                    break;
                }
                if(Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(i, transform.up)*transform.forward, out hit, sensorLength+5f))
        {
        Debug.DrawLine(sensorStartPos, hit.point);
        
        }
        else
        {   
            Debug.Log("Right");
            avoidMultiplier=1.3f;
            isBraking=false;
            break;
            }
        }}
        if(avoiding)
        {

            targetSteerAngle=MaxSteerAngle*avoidMultiplier;
        //    wheelFL.steerAngle=MaxSteerAngle*avoidMultiplier;
            
          //  wheelFR.steerAngle=MaxSteerAngle*avoidMultiplier;
        }
    }
    private void ApplySteer()
    {
        if(avoiding)
        return;
    Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
    relativeVector /= relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * MaxSteerAngle;
    targetSteerAngle=newSteer*reversingMultiplier;
   // wheelFL.steerAngle = newSteer*reversingMultiplier;
   // wheelFR.steerAngle = newSteer*reversingMultiplier;
    wheelL.transform.localEulerAngles=new Vector3(0f, newSteer+180, 0f);
            wheelR.transform.localEulerAngles=new Vector3(0f, newSteer, 0f);

    }
     private void Reversing()
    {
        if(isReversingCollision)
        {   
            reverseCar=true;
            isBraking=false;
            isBrakingDistance=false;
            motorTorqueMultiplier=-1;
        }
        else if(isReversing)
        {
            
            reverseCar=true;
            isBraking=false;
            motorTorqueMultiplier=-1;
        }
        else
        {
          //  Debug.Log("NotReversing");
            reverseCar=false;
            motorTorqueMultiplier=1;
        }
    }
private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if(((currentSpeed < maxSpeed)&&(currentSpeed>-maxSpeed))&&((!isBraking)||(!isBrakingDistance)))
        {
            wheelFL.motorTorque = maxMotorTorque*motorTorqueMultiplier;
            wheelFR.motorTorque = maxMotorTorque*motorTorqueMultiplier;
        }
        else if(reverseCar)
        {
            wheelFL.motorTorque = maxMotorTorque*motorTorqueMultiplier;
            wheelFR.motorTorque = maxMotorTorque*motorTorqueMultiplier;
        }
        
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
        foreach(GameObject mesh in MeshesR)
       {
           mesh.transform.Rotate(rb.velocity.magnitude*(transform.InverseTransformDirection(rb.velocity).z >=0?1:(-1))/(2*Mathf.PI*0.15f), 0f, 0f);
       }
        foreach(GameObject mesh in MeshesL)
       {mesh.transform.Rotate(rb.velocity.magnitude*(transform.InverseTransformDirection(rb.velocity).z >=0?(1):(-1))/(-2*Mathf.PI*0.15f), 0f, 0f);
       }
    }

private void CheckWaypointDistance()
    {  // Debug.Log(currentNode);
        if(aiFollowChoice==false)
        {
            if(Vector3.Distance(transform.position, nodes[currentNode].position)<5f)
        {
            isBrakingDistance=true;
        }
            else
            isBrakingDistance=false;
        }
        else
        {
            if(Vector3.Distance(transform.position, nodes[currentNode].position)<5f)
        {
             if(currentNode== nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
        
        }
       // Debug.Log(Vector3.Distance(transform.position, nodes[currentNode].position));
    }

    private void Braking()
    {

        if((isBraking||isBrakingDistance))
        {
        //     Debug.Log("Braking");
            wheelRL.brakeTorque=maxBrakeTorque;
            wheelRR.brakeTorque=maxBrakeTorque;
            wheelFR.brakeTorque=maxBrakeTorque;
            wheelFL.brakeTorque=maxBrakeTorque;
        }
        else
        {
            wheelRL.brakeTorque=0;
            wheelRR.brakeTorque=0;
            wheelFL.brakeTorque=0;
            wheelFR.brakeTorque=0;
        }
    }
    private void lerpTurn()
    {
        wheelFL.steerAngle=Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime*turnSpeed);
        
        wheelFR.steerAngle=Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime*turnSpeed);
    }
   

}