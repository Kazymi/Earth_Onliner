using System.Collections.Generic;
using UnityEngine;

public class BuildingRadar : MonoBehaviour
{
    [SerializeField] private BuildingContractor buildingContractor;
    [SerializeField] private List<Turret> turrets;
    [SerializeField] private float cooldown;
    [SerializeField] private float radius;

    private float _currentCooldown;

    private void Start()
    {
        if (buildingContractor.PhotonView.ViewID == 0)
        {
            Destroy(this);
        }
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
            if (enemies[i].transform == transform
                || damageable == null)
            {
                continue;
            }

            if (damageable.IsMine == buildingContractor.IsMine)
            {
                continue;
            }

            foreach (var turret in turrets)
            {
                turret.RotateToTransform(damageable.TargetTransform);
            }

            _currentCooldown = cooldown;
            return;
        }

        foreach (var turret in turrets)
        {
            turret.RotateToTransform(null);
        }

        _currentCooldown = cooldown;
    }
}