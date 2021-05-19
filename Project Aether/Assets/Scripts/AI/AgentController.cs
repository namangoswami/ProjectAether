using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.AI.NavMeshAgent Agent;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(target.position);
    }
}
