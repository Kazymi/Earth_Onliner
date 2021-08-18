using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBuilding : MonoBehaviour
{
    [SerializeField] private float radiusBuilding;
    [SerializeField] private BuildingResource resourcesForBuilding;

    public BuildingResource Resource => resourcesForBuilding;
    public float RadiusBuilding => radiusBuilding;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(radiusBuilding, 1, radiusBuilding));
    }
    
    public bool CheckPosition()
    {
        var returnValue = true;
        var faundResources = false;
        var allFindGameObject =
            Physics.OverlapBox(transform.position,
                new Vector3(RadiusBuilding, RadiusBuilding, RadiusBuilding));

        foreach (var findGameObject in allFindGameObject)
        {
            var building = findGameObject.GetComponent<BuildingContractor>();
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

                if (resource.BuildingResources == Resource)
                {
                    faundResources = true;
                    break;
                }
            }
        }

        if (Resource == BuildingResource.Nothing)
        {
            faundResources = true;
        }

        returnValue = faundResources && returnValue;
        return returnValue;
    }
}
