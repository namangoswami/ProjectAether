using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnTile2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float nextStep=0;
    public List<GameObject> tile;
    public Transform startPos;
    public Transform player;
    public float xDistance=0, xDClone=0;
    // Update is called once per frame
    void Update()
    {
        if(nextStep<100)
       { nextStep+=xDClone;
        Instantiate(tile[0], new Vector3(startPos.position.x, startPos.position.y, transform.position.z+nextStep), Quaternion.identity);
     }
   /* for(int i=0;i<100;i++)
    {
        xDistance+=xDClone;
        Instantiate(tile[1], new Vector3(startPos.position.x+xDistance, startPos.position.y, transform.position.z), Quaternion.identity);
 
    }*/
   
    }
}
