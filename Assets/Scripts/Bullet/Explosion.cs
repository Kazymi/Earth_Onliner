using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour,IFactoryInitialize
{
    [SerializeField] private float destroyTimer;
    [SerializeField] private GameObject explosionEffectGameObject;
    private float _currentTimer;

    public void Initialize(float radius, float damage)
    {
        explosionEffectGameObject.SetActive(true);
        _currentTimer = destroyTimer;
        Burst(radius, damage);
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

    private void Burst(float radius, float damage)
    {
        var findGameObjects = Physics.OverlapSphere(transform.position, radius);
        foreach (var findGameObject in findGameObjects)
        {
            var health = findGameObject.GetComponent<IDamageable>();
            var npc = findGameObject.GetComponent<NPC>();
            var building = findGameObject.GetComponent<BuildingContractor>();
            if (health != null)
            {
                if (npc)
                {
                    if (npc.IsMine == false)
                    {
                        health.TakeDamage(damage);
                    }
                }

                if (building)
                {
                    if (building.IsMine == false)
                    {
                        health.TakeDamage(damage);
                    }
                }
            }
        }
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