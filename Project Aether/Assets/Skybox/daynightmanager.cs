using UnityEngine;

[ExecuteAlways]
public class daynightmanager : MonoBehaviour
{
    //references
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private lightingpreset Preset;
    //variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    
    private void Update()
    {
        if (Preset == null)
            return;
        if(Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime * 0.1f; //I added *0.1f to elongate the day manipulate as you see fit
            TimeOfDay %= 24; // clamp between 0-24 (???)
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }
    
    
    
    
    
    
    private void UpdateLighting( float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if(DirectionalLight !=null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -170, 0));     
        }    



    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;
        if (RenderSettings.sun!=null)
        {
            DirectionalLight = RenderSettings.sun;
        }

        else
        {
          Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return; 
                }
            }
        }
    }



}