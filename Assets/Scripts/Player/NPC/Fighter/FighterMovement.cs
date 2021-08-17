using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FighterMovement : MonoBehaviour,IMovement
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform fighterGameObject;
    [SerializeField] private Vector3 turnFlyingFighter;
    [SerializeField] private Vector3 turnFightFighter;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speedDuringBattle;
    [SerializeField] private float toRotateValue = 40f;

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
            if (Vector3.Distance(transform.position, _target.position) < toRotateValue)
            {
                if (_agent.enabled)
                {
                    _agent.ResetPath();
                    _agent.enabled = false;
                }
                var lookDir = _target.position - transform.position;
                lookDir.y = 0;
                transform.rotation = Quaternion.LookRotation(lookDir);
                fighterGameObject.transform.localRotation = Quaternion.Lerp(fighterGameObject.localRotation,
                    Quaternion.Euler(turnFightFighter), rotateSpeed * Time.deltaTime);
                transform.RotateAround(_target.position, Vector3.down, speedDuringBattle * Time.deltaTime);
            }
            else
            {
                if (_agent.enabled == false)
                {
                    _agent.enabled = true;
                }
                fighterGameObject.transform.localRotation = Quaternion.Lerp(fighterGameObject.localRotation,
                    Quaternion.Euler(turnFlyingFighter), rotateSpeed * Time.deltaTime);
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
        if (isMine == false)
        {
            Destroy(this);
        }
    }
}
