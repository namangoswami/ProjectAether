
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "lightingpreset" , menuName = "Scriptables/Lightning Preset",order = 1)]

public class lightingpreset : ScriptableObject 
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
}
