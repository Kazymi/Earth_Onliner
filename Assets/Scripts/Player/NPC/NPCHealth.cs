using System;
using Photon.Pun;
using UnityEngine;

public class NPCHealth : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] private bool Dead;
    [SerializeField] private GameObject deadGameObject;
    [SerializeField] private float health;
    [SerializeField] private bool isMine;
    [SerializeField] private Transform target;
    
    private PhotonView _photonView;
    private DeadNPCManager _deadNpcManager;
    public bool IsMine
    {
        get => isMine;
        set => isMine = value;
    }

    private float _currentHealth;
    public Transform TargetTransform { get => target; set => target = value; }

    private void Update()
    {
        if(Dead) Death();
    }

    private void Start()
    {
        _deadNpcManager = ServiceLocator.GetService<DeadNPCManager>();
        _photonView = GetComponent<PhotonView>();
        _currentHealth = health;
        if (TargetTransform == null)
        {
            TargetTransform = transform;
        }
        if (_photonView.ViewID == 0)
        {
            Destroy(this);
            return;
        }
        Initialize();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Debug.Log("TakeDamage");
            _photonView.RPC(RPCEvents.Death.ToString(), RpcTarget.All);
        }
    }


    [PunRPC]
    public void Death()
    {
        if (deadGameObject != null)
        {
            _deadNpcManager.SpawnDeadNPC(target,deadGameObject.name);
        }
        Destroy(gameObject);
    }

    public void Initialize()
    {
        if (isMine)
        {
            gameObject.layer = LayerMask.NameToLayer(LayerType.Friendly.ToString());
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer(LayerType.Enemy.ToString());
        }
    }
}