using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ResourceForConstruction : MonoBehaviour
{
    [SerializeField] private BuildingResource buildingResource;
    
    public BuildingResource BuildingResources => buildingResource;
}
