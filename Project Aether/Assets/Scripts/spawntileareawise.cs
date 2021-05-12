using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawntileareawise : MonoBehaviour
{
    // Start is called before the first frame update
      public float nextStep=0;
    public List<GameObject> tile;
    public Transform startPos;
    public float xDistance=0, xDClone=0;
    // Update is called once per frame
    void Update()
    { 
      if(nextStep<xDistance)
      {
        nextStep+=xDClone;
       Instantiate(tile[0], new Vector3(startPos.position.x+nextStep, startPos.position.y, transform.position.z), Quaternion.identity);
      }}
}
