using UnityEngine;

public class RocketAmmo : MonoBehaviour,IAmmo
{
    [SerializeField] private float flySpeed;
    [SerializeField] private TurretConfiguration turretConfiguration;
    [SerializeField] private float radiusExplosion;
    [SerializeField] private float damage;
    private AmmoManager _ammoManager;

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
        Debug.Log("OnTriggher");
        var explosionGameObject = _ammoManager.GetAmmoByTurretType(turretConfiguration);
        var explosion = explosionGameObject.GetComponent<Explosion>();
        if (explosion)
        {
            explosion.transform.position = transform.position;
            explosion.Initialize(radiusExplosion,damage);   
        }
        ParentFactory.Destroy(gameObject);
    }
    public Factory ParentFactory { get; set; }
}
