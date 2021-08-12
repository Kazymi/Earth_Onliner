using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NPCHealth : MonoBehaviour,IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private bool isMine;
    
    private PhotonView _photonView;
    public bool IsMine
    {
        get => isMine;
        set => isMine = value;
    }

    private float _currentHealth;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _currentHealth = health;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _photonView.RPC(RPCEvents.Death.ToString(),RpcTarget.All);
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
            gameObject.layer = 8;
        }
        else
        {
            gameObject.layer = 9;
        }
    }
}
