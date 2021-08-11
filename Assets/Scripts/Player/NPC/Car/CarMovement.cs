using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour, IMovement,INPC
{
    [SerializeField] private Transform target;
    private Builders _builders;
    private NavMeshAgent _agent;
    private Transform _target;

    private void Start()
    {
        _builders = ServiceLocator.GetService<Builders>();
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(ClosestTarget(_builders.GetPositionEnemyBuilding(transform.position)));
    }

    private void Update()
    {
        if (_target != null)
        {
            _agent.SetDestination(_target.position);
        }
        else
        {
            StartCoroutine(ClosestTarget(_builders.GetPositionEnemyBuilding(transform.position)));
        }
    }

    public void SetNewTarget(Transform target)
    {
        _target = target;
    }

    IEnumerator ClosestTarget(Transform targets)
    {
        if (targets == null)
        {
            _target = target;
            yield break;
        }

        while (_agent.isOnNavMesh == false)
        {
            _agent.enabled = false;
            yield return null;
            _agent.enabled = true;
        }

        _target = targets;
    }

    public void Initialize(bool isMine)
    {
        if (isMine)
        {
            gameObject.layer = 0;
            
        }
        else
        {
            Destroy(this);
        }
    }
}