using System;
using Photon.Pun;
using UnityEngine;

public class NPCHealth : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] private GameObject deadGameObject;
    [SerializeField] private float health;
    [SerializeField] private bool isMine;
    [SerializeField] private Transform target;
    
    private PhotonView _photonView;
    private DeadNPCManager _deadNpcManager;
    private event Action _npcDeath;
    private float _currentHealth;
    public Transform TargetTransform { get => target; set => target = value; }

    public bool IsMine
    {
        get => isMine;
        set => isMine = value;
    }
    
    public event  Action NPCDeath
    {
        add => _npcDeath += value;
        remove => _npcDeath -= value;
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
        _npcDeath?.Invoke();
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