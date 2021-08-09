using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovenment : MonoBehaviour
{
    private Builders _builders;
    private NavMeshAgent _agent;
    private Transform _target;

    private void Start()
    {
        _builders = ServiceLocator.GetService<Builders>();
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(ClosestTarget(_builders.GetEnemyBuildersPosition()));
    }

    private void Update()
    {
        if (_target != null)
        {
            _agent.SetDestination(_target.position);
        }
    }

    IEnumerator ClosestTarget(List<Transform> targets)
    {
        while (_agent.isOnNavMesh == false)
        {
            _agent.enabled = false;
            yield return null;
            _agent.enabled = true;
        }
        yield return new WaitForSeconds(1f);
        float tmpDist = float.MaxValue;
        for (int i = 0; i < targets.Count; i++)
        {
            if (_agent.SetDestination(targets[i].transform.position))
            {
                while (_agent.pathPending)
                {
                    yield return null;
                }
                if (_agent.pathStatus != NavMeshPathStatus.PathInvalid)
                {
                    float pathDistance = 0;
                    pathDistance += Vector3.Distance(transform.position, _agent.path.corners[0]);
                    for (int j = 1; j < _agent.path.corners.Length; j++)
                    {
                        pathDistance += Vector3.Distance(_agent.path.corners[j - 1], _agent.path.corners[j]);
                    }

                    if (tmpDist > pathDistance)
                    {
                        tmpDist = pathDistance;
                        _target = targets[i];
                        _agent.ResetPath();
                    }
                }
                else
                {
                    //not found building
                }
            }
        }
    }
}