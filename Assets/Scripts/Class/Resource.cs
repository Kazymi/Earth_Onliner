using System;
using UnityEngine;

[Serializable]
public class Resource
{
    [SerializeField] private TypeResource typeResource;
    [SerializeField] private int amount;

    public TypeResource TypeResource => typeResource;
    public int Amount => amount;
}