using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class BuildingContractor : MonoBehaviour
{
    [SerializeField] private LayerMask earthMask;
    [SerializeField] private float radiusBuilding;
    [SerializeField] private BuildingResource resourcesForBuilding;

    private InputHandler _inputHandler;
    public BuildingResource Resource => resourcesForBuilding;
    public float RadiusBuilding => radiusBuilding;
    
    private void Start()
    {
        _inputHandler = ServiceLocator.GetService<InputHandler>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(radiusBuilding, 1, radiusBuilding));
    }
}