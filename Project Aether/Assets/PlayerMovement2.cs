using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public Rigidbody rb;
private bool turnLeft, turnRight, pressW, pressS;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(0,0  , 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        turnLeft=Input.GetKeyDown(KeyCode.A);
        turnRight=Input.GetKeyDown(KeyCode.D);
        pressW=Input.GetKeyDown(KeyCode.W);
        pressS=Input.GetKeyDown(KeyCode.S);
        
        if(turnLeft)
       { 
           transform.Rotate(new Vector3(0f, -90f, 0f));}
        else if(turnRight){
      transform.Rotate(new Vector3(0f, 90f, 0f));}
      if(pressW)
      {
          
rb.AddForce ((transform.forward) * 20000*Time.deltaTime);
      }
      if(pressS)
      {rb.AddForce (-(transform.forward) * 20000*Time.deltaTime);
      }
        
       
    }
}