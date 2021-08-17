using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadNPCManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> deadNpcGameObjects;
    [SerializeField] private int amount;
    private Dictionary<string, Factory> _factories = new Dictionary<string, Factory>();

    private void OnEnable()
    {
        ServiceLocator.Subscribe<DeadNPCManager>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<DeadNPCManager>();
    }

    private void Start()
    {
        foreach (var gameObject in deadNpcGameObjects)
        {
            _factories.Add(gameObject.name,new Factory(gameObject,amount,transform));
        }
    }

    public void SpawnDeadNPC(Transform transform, string name)
    {
        var newGameObject = _factories[name].Create();
        var iFactoryInitialize = newGameObject.GetComponent<IFactoryInitialize>();
        if (iFactoryInitialize != null)
        {
            iFactoryInitialize.Initialize();
            iFactoryInitialize.ParentFactory = _factories[name];
        }
        newGameObject.transform.position = transform.position;
        newGameObject.transform.rotation = transform.rotation;
    }
}
