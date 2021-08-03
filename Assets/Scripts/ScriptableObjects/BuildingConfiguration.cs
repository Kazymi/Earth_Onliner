using UnityEngine;

[CreateAssetMenu(fileName = "New building", menuName = "Building Configuration")]
public class BuildingConfiguration : ScriptableObject
{
    [SerializeField] private GameObject buildingGameObject;
    [SerializeField] private int maxAmount;
    [SerializeField] private int amountInFactory;
    public GameObject BuildingGameObject => buildingGameObject;
    public int AmountInFactory => amountInFactory;
    public int MaxAmount => maxAmount;
    
}