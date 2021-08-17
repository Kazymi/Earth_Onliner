using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider),typeof(Rigidbody))]
public class CasualAmmo : MonoBehaviour,IFactoryInitialize
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
        StopAllCoroutines();
        ParentFactory.Destroy(gameObject);
    }

    public Factory ParentFactory { get; set; }
    
    public void Initialize()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10f);
        ParentFactory.Destroy(gameObject);
    }
}
