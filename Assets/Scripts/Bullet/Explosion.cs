using UnityEngine;

public class Explosion : MonoBehaviour, IAmmo
{
    [SerializeField] private float destroyTimer;
    [SerializeField] private GameObject explosionEffectGameObject;
    private float _currentTimer;

    public void Initialize(float radius, float damage)
    {
        explosionEffectGameObject.SetActive(true);
        Explositon(radius,damage);
    }

    private void Update()
    {
        if (_currentTimer <= 0)
        {
            explosionEffectGameObject.SetActive(false);
            ParentFactory.Destroy(gameObject);
        }
        _currentTimer -= Time.deltaTime;
    }

    private void Explositon(float radius, float damage)
    {
        _currentTimer = destroyTimer;
        var findGameObjects = Physics.OverlapSphere(transform.position, radius,LayerMask.NameToLayer(LayerType.Friendly.ToString()));
        foreach (var findGameObject in findGameObjects)
        {
            var health = findGameObject.GetComponent<IDamageable>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    public Factory ParentFactory { get; set; }
}