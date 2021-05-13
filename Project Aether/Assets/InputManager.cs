using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float throttle;
    public float steer;
    public bool l;
<<<<<<< HEAD
    public bool tl;
=======
    public bool brake;
>>>>>>> 1abba7bd99370786177ae8cb5576340e6762a21d

    // Update is called once per frame
    void Update()
    {
        throttle=Input.GetAxis("Vertical");
        steer=Input.GetAxis("Horizontal");
        brake=Input.GetKey(KeyCode.Space);

        l = Input.GetKeyDown(KeyCode.L);
        tl = Input.GetKey(KeyCode.S);
    }
}
