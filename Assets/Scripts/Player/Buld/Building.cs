using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class Building : MonoBehaviour
{
    [SerializeField] private float radiusBuilding;
    [SerializeField] private BuildingResource resourcesForBuilding;

    private bool _isConstructionAllowed;
    private InputHandler _inputHandler;


    public void Initialize()
    {
        Destroy(gameObject);
    }

    private void OnMouseDrag()
    {
        var pos = _inputHandler.GetBuildPosition();
        if (pos.Position == Vector3.zero)
        {
            return;
        }

        transform.position = pos.Position;
        transform.rotation = Quaternion.LookRotation(pos.Normal);
    }

    private void Start()
    {
        _inputHandler = ServiceLocator.GetService<InputHandler>();
    }

    public bool CheckPosition()
    {
        var findIsland = false;
        var allFindGameObject =
            Physics.OverlapBox(transform.position, new Vector3(radiusBuilding, radiusBuilding, radiusBuilding));

        foreach (var findGameObject in allFindGameObject)
        {
            var building = findGameObject.GetComponent<Building>();
            var resource = findGameObject.GetComponent<ResourceForConstruction>();

            if (building)
            {
                if (building != this)
                {
                    return false;
                }
            }

            if (resource)
            {
                if (resource.BuildingResources == BuildingResource.Water)
                {
                    return false;
                }

                if (resource.BuildingResources == resourcesForBuilding)
                {
                    return true;
                }
            }

            if (resourcesForBuilding == BuildingResource.Nothing)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(radiusBuilding, 1, radiusBuilding));
    }
}