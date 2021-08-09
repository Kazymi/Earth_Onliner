using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New turret", menuName = "Turret Configuration")]
public class TurretConfiguration : ScriptableObject
{
    [SerializeField] private GameObject ammoGameObject;
    [SerializeField] private float fireRate;

    public GameObject AmmoGameObject => ammoGameObject;
    public float FireRate => fireRate;
}
