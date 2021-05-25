using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI frameCounter;
    public bool FPS = true;
    public virtual void changeText(float speed)
    {
        float s = speed * (speed >= 0f ? (3.6f) : (-3.6f));
        text.text = Mathf.Round(s) + "";
    }
    void Update()
    {
        frameCounter.text = (Mathf.Round(1f / Time.deltaTime)) + "";
    }
}
