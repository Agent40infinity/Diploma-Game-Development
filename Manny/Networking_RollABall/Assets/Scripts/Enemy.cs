using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Mirror;


public class Enemy : NetworkBehaviour
{
    public string targetTag = "Target";
    public Transform target;
    private NavMeshAgent agent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    public void Update()
    {
        agent.SetDestination(target.position);
    }
}
