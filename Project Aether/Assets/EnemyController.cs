using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Target;
    public Rigidbody rb;
    public List<WheelCollider> throttleWheels;
    public List<GameObject> steeringWheel;
    public List<GameObject> MeshesL, MeshesR;
    public float strenghtCoefficient=10000f, brakeCoefficient=100000000f;
    float distanceZ, distanceX;
    public float speed=30, rotatingSpeed=8;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* distanceZ=(Target.transform.position.z -transform.position.z );
        
        distanceX=(Target.transform.position.x -transform.position.x );
     //   Debug.Log(Target.transform.position-transform.position);
     //   Debug.Log(distanceZ);

        if(distanceZ>0)
        {
           // Debug.Log("Accelerating");
                foreach(WheelCollider wheel in throttleWheels)
                {
                    
                 wheel.motorTorque=strenghtCoefficient*Time.deltaTime;
                }
        }
        
        if(Target.transform.forward!=transform.forward)
        {
            float perp=Mathf.Sqrt((distanceZ*distanceZ)+(distanceX*distanceX));
                float angle=Mathf.Acos(distanceZ/perp)*57.2958f;
transform.Rotate(0, angle, 0);
        }*/
        Vector3 pointTarget=transform.position-Target.transform.position;
        pointTarget.Normalize();
        float value=Vector3.Cross(pointTarget, transform.forward).y;
        rb.angularVelocity=rotatingSpeed*value*new Vector3(0, 1, 0);
        rb.velocity=transform.forward*speed;
        
    }
}
