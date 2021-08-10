using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private bool isMine;

    public bool IsMine
    {
        get => isMine;
        set => isMine = value;
    }

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = health;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Death();
    }

    public void Death()
    {
        // TODO: use pool
        Destroy(gameObject);
    }
}