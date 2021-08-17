using System.Collections;
using UnityEngine;

public class RocketAmmo : MonoBehaviour,IFactoryInitialize
{
    [SerializeField] private float flySpeed;
    [SerializeField] private TurretConfiguration turretConfiguration;
    [SerializeField] private float radiusExplosion;
    [SerializeField] private float damage;
    private AmmoManager _ammoManager;

    public Factory ParentFactory { get; set; }
    private void Start()
    {
        _ammoManager = ServiceLocator.GetService<AmmoManager>();
    }

    private void Update()
    {
        transform.position += transform.forward * flySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var explosionGameObject = _ammoManager.GetAmmoByTurretType(turretConfiguration);
        var explosion = explosionGameObject.GetComponent<Explosion>();
        if (explosion)
        {
            explosion.transform.position = transform.position;
            explosion.Initialize(radiusExplosion,damage);   
            StopAllCoroutines();
        }
        ParentFactory.Destroy(gameObject);
    }

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
