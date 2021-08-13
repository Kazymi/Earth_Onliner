using System.Collections.Generic;
using UnityEngine;

public class NPCRadar : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private float radius;
    [SerializeField] private List<Turret> turrets;
    [SerializeField] private NPC _npc;
    private IMovement _npcMovement;
    private IDamageable _damageable;
    private float _currentCooldown;

    private void Start()
    {
        if (_npc == null)
        {
            return;
        }
        _npcMovement = _npc.Movement;
        _damageable = _npc.Health;
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
            if (enemies[i].transform == transform)
            {
                continue;
            }

            if (damageable == null)
            {
                continue;
            }

            if (_npc)
            {
                if (damageable.IsMine == _npc.IsMine)
                {
                    continue;
                }
            }
            else
            {
                if (damageable.IsMine)
                {
                    continue;
                }
            }

            if (_damageable != null)
            {
                if (damageable == _damageable)
                {
                    continue;
                }
            }
            foreach (var turret in turrets)
            {
                turret.RotateToTransform(damageable.TargetTransform);
            }
            _currentCooldown = cooldown;
            _npcMovement?.SetNewTarget(damageable.TargetTransform);
            return;
        }

        foreach (var turret in turrets)
        {
            turret.RotateToTransform(null);
        }
        _currentCooldown = cooldown;
    }
}