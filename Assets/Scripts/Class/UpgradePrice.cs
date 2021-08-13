using System;
using UnityEngine;

[Serializable]
public class UpgradePrice
{
    [SerializeField] private int needGold;
    [SerializeField] private int needWood;
    [SerializeField] private int needIron;

    public int NeedGold => needGold;

    public int NeedWood => needWood;

    public int NeedIron => needIron;

}
