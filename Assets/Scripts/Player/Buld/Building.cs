using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private float radiusBuilding;
    [SerializeField] private BuildingResource resourcesForBuilding;
    [SerializeField] private bool needPrivate;

    private bool _isConstructionAllowed;
    private bool _isBuild;

    public void Initialize()
    {
        _isBuild = true;
    }

    public CheckedPosition CheckPosition()
    {
        var findIsland = false;
        var checkedPosition = new CheckedPosition();
        var allFindGameObject = 
            Physics.OverlapBox(transform.position, new Vector3(radiusBuilding, radiusBuilding, radiusBuilding));
        
        foreach (var findGameObject in allFindGameObject)
        {
            var building = findGameObject.GetComponent<Building>();
            var resource = findGameObject.GetComponent<ResourceForConstruction>();
            var island = findGameObject.GetComponent<Island>();

            if (building)
            {
                if (building != this)
                {
                    checkedPosition.UnlockPosition = false;
                    break;
                }
            }

            // if (island)
            // {
            //     checkedPosition.FindIsland = island;
            //     if (needPrivate)
            //     {
            //         if (island.IsUnlockIsland)
            //         {
            //             continue;
            //         }
            //         else
            //         {
            //             checkedPosition.UnlockPosition = false;
            //             break;
            //         }
            //     }
            // }

            if (resource)
            {
                if (resource.BuildingResources == BuildingResource.Water)
                {
                    checkedPosition.UnlockPosition = false;
                    break;
                }
            
                if (resource.BuildingResources == resourcesForBuilding)
                {
                    checkedPosition.UnlockPosition = true;
                    break;
                }
            }

            if (resourcesForBuilding == BuildingResource.Nothing)
            {
                checkedPosition.UnlockPosition = true;
                break;
            }
        }

        //if (checkedPosition.FindIsland == null) checkedPosition.UnlockPosition = false;
        return checkedPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(radiusBuilding, 1, radiusBuilding));
    }
}