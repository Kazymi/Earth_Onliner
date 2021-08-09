using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider),typeof(Rigidbody))]
public class CasualAmmo : MonoBehaviour,IAmmo
{
    [SerializeField] private float flySpeed;
    [SerializeField] private float damage;

    private void Update()
    {
        transform.position += transform.forward * flySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
        ParentFactory.Destroy(gameObject);
    }

    public Factory ParentFactory { get; set; }
}
