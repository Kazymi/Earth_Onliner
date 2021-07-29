using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New building", menuName = "Building Configuration")]
public class BuildingConfiguration : ScriptableObject
{
    [SerializeField] private GameObject buildingGameObject;
    [SerializeField] private int amountInFactory;
    [SerializeField] private List<Resource> resourcesList;

    public GameObject BuildingGameObject => buildingGameObject;
    public int AmountInFactory => amountInFactory;
    public List<Resource> ResourcesList => resourcesList;
}