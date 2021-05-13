using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public List<Light> lightsF, lightsT;
    public float LightIntesity = 2f, LightIntesityF=2f;
    public Rigidbody rb;

    public virtual void ToggleHeadlights()
    {
        foreach(Light light in lightsF)
        {
            light.intensity = light.intensity == 0 ? LightIntesityF : 0;
        }
        foreach (Light light in lightsT)
        {
            light.intensity = light.intensity == 0 ? LightIntesity : 0;
        }
    }

    public virtual void ToggleBrakelightsOn(bool trigger)
    {
       
        foreach(Light light in lightsT)
        {


            if (rb.velocity.z < 0)
            {
                light.color = Color.white;
                light.intensity = 1f;
            }
            else
            {
                light.color = Color.red;
                light.intensity = 3;
            }
        }
    }
    public virtual void ToggleBrakelightsOff()
    {
      
        foreach (Light light in lightsT)
        {

            light.intensity = LightIntesity;
            light.color = Color.red;



        }
    }

}
