using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HelicopterMovement : MonoBehaviour, IMovement,INPC
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform helicopterGameObject;
    [SerializeField] private Vector3 turnFlyingHelicopter;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private LayerMask friendlyLayer;
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
            if (Vector3.Distance(transform.position, _target.position) < 25f)
            {
                _agent.ResetPath();
                var lookDir = _target.position - transform.position;
                lookDir.y = 0;
                transform.rotation = Quaternion.LookRotation(lookDir);
                transform.RotateAround(_target.position, Vector3.down, _agent.speed * Time.deltaTime);
                helicopterGameObject.transform.localRotation = Quaternion.Lerp(helicopterGameObject.localRotation,
                    Quaternion.identity, rotateSpeed * Time.deltaTime);
            }
            else
            {
                helicopterGameObject.transform.localRotation = Quaternion.Lerp(helicopterGameObject.localRotation,
                    Quaternion.Euler(turnFlyingHelicopter), rotateSpeed * Time.deltaTime);
                _agent.SetDestination(_target.position);
            }
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
            gameObject.layer = friendlyLayer;
        }
        else
        {
            Destroy(this);
        }
    }
}