using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HelicopterMovement : MonoBehaviour, IMovement
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform helicopterGameObject;
    [SerializeField] private Vector3 turnFlyingHelicopter;
    [SerializeField] private float rotateSpeed;
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
                if (_agent.enabled)
                {
                    _agent.ResetPath();
                    _agent.enabled = false;
                }
                var lookDir = _target.position - transform.position;
                lookDir.y = 0;
                transform.rotation = Quaternion.LookRotation(lookDir);
                transform.RotateAround(_target.position, Vector3.down, _agent.speed * Time.deltaTime);
                helicopterGameObject.transform.localRotation = Quaternion.Lerp(helicopterGameObject.localRotation,
                    Quaternion.identity, rotateSpeed * Time.deltaTime);
            }
            else
            {
                if (_agent.enabled == false)
                {
                    _agent.enabled = true;
                }
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
        while (_agent.isOnNavMesh == false)
        {
            _agent.enabled = false;
            yield return null;
            _agent.enabled = true;
        }
        if (targets == null)
        {
            _target = target;
            yield break;
        }
        _target = targets;
    }


    public void Initialize(bool isMine)
    {
        if (isMine == false)
        {
            Destroy(this);
        }
    }
}