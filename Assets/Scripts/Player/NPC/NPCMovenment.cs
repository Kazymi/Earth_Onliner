using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovenment : MonoBehaviour
{
    [SerializeField] private Transform testTarget;

    private NavMeshAgent _agent;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.Move(testTarget.position);
    }

    private void Update()
    {
        _agent.SetDestination(testTarget.position);
    }
}