using UnityEngine;

[CreateAssetMenu(fileName = "New buy config", menuName = "Buy Configuration")]
public class BuyConfiguration : ScriptableObject
{
    [SerializeField] private BuildingConfiguration buildingConfiguration;
    [SerializeField] private int maxAmount;
    [SerializeField] private int needWood;
    [SerializeField] private int needGold;
    [SerializeField] private int needIron;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    public BuildingConfiguration BuildingConfiguration => buildingConfiguration;
    public int MaxAmount => maxAmount;
    public int NeedWood => needWood;
    public int NeedGold => needGold;
    public int NeedIron => needIron;
    public string Description => description;
    public Sprite Icon => icon;
}