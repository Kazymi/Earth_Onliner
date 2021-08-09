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

    private float _currentCooldown;

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
            var damageDealer = enemies[i].GetComponent<IDamageDealer>();
            if(enemies[i].transform == transform) continue;
            if (damageDealer != null)
            {
                if (damageDealer.IsMine == false)
                {
                    foreach (var turret in turrets)
                    {
                        turret.RotateToTransform(enemies[i].transform);
                    }
                    _currentCooldown = cooldown;
                    return;
                }
            }
        }

        foreach (var turret in turrets)
        {
            turret.RotateToTransform(null);
        }
        _currentCooldown = cooldown;
    }
}