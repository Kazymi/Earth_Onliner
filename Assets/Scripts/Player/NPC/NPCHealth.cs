using Photon.Pun;
using UnityEngine;

public class NPCHealth : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private bool isMine;
    [SerializeField] private Transform target;
    private PhotonView _photonView;

    public bool IsMine
    {
        get => isMine;
        set => isMine = value;
    }

    private float _currentHealth;
    public Transform TargetTransform { get => target; set => target = value; }

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _currentHealth = health;
        if (TargetTransform == null)
        {
            TargetTransform = transform;
        }
        if (_photonView.ViewID == 0)
        {
            Destroy(this);
        }
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