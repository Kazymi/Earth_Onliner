using System;
using UnityEngine;

[Serializable]
public class Resource
{
    [SerializeField] private TypeResource typeResource;
    [SerializeField] private int amount;

    public TypeResource TypeResource => typeResource;
    public int Amount
    {
        get => amount;
        set => amount = value;
    }
    
    public Resource(TypeResource typeResource)
    {
        this.typeResource = typeResource;
        amount = 0;
    } 
    public Resource(Resource resource)
    {
        typeResource = resource.typeResource;
        amount = resource.amount;
    }
}