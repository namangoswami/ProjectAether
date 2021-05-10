using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float throttle;
    public float steer;

    // Update is called once per frame
    void Update()
    {
        throttle=Input.GetAxis("Vertical");
        steer=Input.GetAxis("Horizontal");
    }
}
