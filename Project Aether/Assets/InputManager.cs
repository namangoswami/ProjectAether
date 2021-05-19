using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float throttle;
    public float steer;
    public bool l;
    public Joystick j;

  //  public button btnSwitch;
    public bool JoystickEnabled=false;

    public bool tl;
  //  public bool switchCam=false;

    public bool brake;
   

    // Update is called once per frame
    void Update()
    {
        
      //  switchCam=false;
        if(!JoystickEnabled)
        {

        throttle=Input.GetAxis("Vertical");
        steer=Input.GetAxis("Horizontal");
        
        }
        else
        {

        throttle=j.Vertical;
        steer=j.Horizontal;
        
        }
        
        brake=Input.GetKey(KeyCode.Space);

        l = Input.GetKeyDown(KeyCode.L);
        tl = Input.GetKey(KeyCode.S);
    }
}
