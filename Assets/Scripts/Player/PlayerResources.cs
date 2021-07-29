using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using UnityEngine;

public class PlayerResources
{
    private Dictionary<TypeResource, int> _resources;

    public PlayerResources()
    {
        _resources = new Dictionary<TypeResource, int>();
        _resources.Add(TypeResource.Gold,0);
        _resources.Add(TypeResource.Iron,0);
        _resources.Add(TypeResource.Wood,0);
    }

    public int GetAmountResource(TypeResource typeResource)
    {
        return _resources[typeResource];
    }
    
    public void AddResource(TypeResource typeResource, int amountResources)
    {
        _resources[typeResource] += amountResources;
        EventBus.RaiseEvent<IChangingAmountResources>(h => h.ChangingAmountResources(typeResource));
    }

    public void RemoveResource(TypeResource typeResource, int amountResources)
    {
        _resources[typeResource] -= amountResources;
        EventBus.RaiseEvent<IChangingAmountResources>(h => h.ChangingAmountResources(typeResource));
    }

    public bool CheckAvailability(TypeResource typeResource, int comparedValue)
    {
        if (_resources[typeResource] >= comparedValue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool CheckAvailability(Resource resource)
    {
        if (_resources[resource.TypeResource] >= resource.Amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}