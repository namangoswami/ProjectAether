using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public GameObject tileToSpawn;
    public GameObject referenceObject;
    public float distanceBetweenTiles=2.0f;
    public float randomValue=0.8f;
    public float timeOffset =0.01f;
    private Vector3 previousTilePosition;
    private float startTime;
    private Vector3 direction, mainDirection=new Vector3(0, 0, 2), otherDirection = new Vector3(2, 0, 0), otherOtherDirection = new Vector3(-2, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        previousTilePosition=referenceObject.transform.position;
        startTime=Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-startTime>timeOffset)
        {
              if(Random.value <0.33333f)
            {
                direction=mainDirection;
            }
            else if(Random.value < 0.6666f)
            {
                Vector3 temp=direction;
                direction=otherDirection;
                mainDirection=direction;
                otherDirection=temp;
            }
            else
            {
                Vector3 temp=direction;
                direction=otherOtherDirection;
                mainDirection=direction;
                otherOtherDirection=temp;
            }
            Vector3 spawnPos=previousTilePosition+distanceBetweenTiles*direction;
            startTime=Time.time;
            Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition=spawnPos;
        }

        
    }
}
