using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<BuildingConfiguration> builders;

    private Dictionary<string, Factory> _factories = new Dictionary<string, Factory>();

    private void OnEnable()
    {
        ServiceLocator.Subscribe<BuildingManager>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<BuildingManager>();
    }

    private void Start()
    {
        foreach (var building in builders)
        {
            _factories.Add(building.name,new Factory(building.BuildingGameObject,building.AmountInFactory,transform));
        }
    }

    public Factory GetFactoryByName(string name)
    {
        return _factories[name];
    }

    public GameObject GetBuildingByName(string name)
    {
        return _factories[name].Create();
    }
}
