using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile2 : MonoBehaviour
{
    // Start is called before the first frame update
    public int nextStep=0;
    public GameObject[] tile;
    public Transform startPos;
    // Update is called once per frame
    void Update()
    {
        nextStep+=6;
        Instantiate(tile[Random.Range(0, tile.Length)], new Vector3(startPos.position.x, startPos.position.y, transform.position.z+nextStep), Quaternion.identity);
    }
}
