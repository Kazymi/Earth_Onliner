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

    private bool _isСonstructionСompleted;
    private InputHandler _inputHandler;


    public void BuildInitialize()
    {
        Destroy(gameObject);
    }

    public void InstantiateInitialize()
    {
        _isСonstructionСompleted = true;
    }

    private void OnMouseDrag()
    {
        if (_isСonstructionСompleted == false)
        {
            var pos = _inputHandler.GetBuildPosition();
            if (pos.Position == Vector3.zero)
            {
                return;
            }

            transform.position = pos.Position;
        }
    }

    private void Start()
    {
        _inputHandler = ServiceLocator.GetService<InputHandler>();
    }

    public bool CheckPosition()
    {
        var returnValue = false;
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
                    returnValue = false;
                    break;
                }
            }

            if (resource)
            {
                if (resource.BuildingResources == BuildingResource.Water)
                {
                    returnValue = false;
                    break;
                }

                if (resource.BuildingResources == resourcesForBuilding)
                {
                    returnValue = true;
                }
            }

            if (resourcesForBuilding == BuildingResource.Nothing)
            {
                returnValue = true;
            }
        }

        return returnValue;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(radiusBuilding, 1, radiusBuilding));
    }
}