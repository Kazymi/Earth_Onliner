using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Upgrade
{
    [SerializeField] private int needGold;
    [SerializeField] private int needWood;
    [SerializeField] private int needIron; 
    [Header("Improvement")]
    [SerializeField] private ResourceGenerate newGold;
    [SerializeField] private ResourceGenerate newWood;
    [SerializeField] private ResourceGenerate newIron;
    
    public int NeedGold => needGold;

    public int NeedWood => needWood;

    public int NeedIron => needIron;

    public ResourceGenerate NewGold => newGold;

    public ResourceGenerate NewWood => newWood;

    public ResourceGenerate NewIron => newIron;
    
}
