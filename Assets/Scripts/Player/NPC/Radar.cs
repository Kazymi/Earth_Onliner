using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private float radius;
    [SerializeField] private List<Turret> turrets;

    private IMovement _npc;
    private float _currentCooldown;

    private void Start()
    {
        _npc = GetComponent<IMovement>() ?? GetComponentInParent<IMovement>();
    }

    private void Update()
    {
        if (_currentCooldown != 0)
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown < 0)
            {
                _currentCooldown = 0;
            }
            return;
        }

        var enemies = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i != enemies.Length; i++)
        {
            var damageable = enemies[i].GetComponent<IDamageable>();
            if(enemies[i].transform == transform) continue;
            if (damageable == null) continue;
            if (damageable.IsMine != false) continue;
            foreach (var turret in turrets)
            {
                turret.RotateToTransform(enemies[i].transform);
            }
            _currentCooldown = cooldown;
            _npc?.SetNewTarget(enemies[i].transform);
            return;
        }

        foreach (var turret in turrets)
        {
            turret.RotateToTransform(null);
        }
        _currentCooldown = cooldown;
    }
}