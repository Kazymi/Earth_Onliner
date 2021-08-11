using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Builders
{
    private Dictionary<BuildingType, List<Transform>>
        _enemyGameObject = new Dictionary<BuildingType, List<Transform>>();

    private Dictionary<BuildingType, List<Transform>> _mainGameObject = new Dictionary<BuildingType, List<Transform>>();

    public void NewBuilding(GameObject building, bool isMine, BuildingType buildingType)
    {
        // if (isMine)
        // {
        //     if (_mainGameObject.ContainsKey(buildingType))
        //     {
        //         _mainGameObject[buildingType].Add(building.transform);
        //         return;
        //     }
        //     _mainGameObject.Add(buildingType, new List<Transform>());
        //     _mainGameObject[buildingType].Add(building.transform);
        // }
        //
        // else
        // {
        //     if (_enemyGameObject.ContainsKey(buildingType))
        //     {
        //         _enemyGameObject[buildingType].Add(building.transform);
        //         return;
        //     }
        //     _enemyGameObject.Add(buildingType, new List<Transform>());
        //     _enemyGameObject[buildingType].Add(building.transform);
        // }
        
        if (_enemyGameObject.ContainsKey(buildingType))
        {
            _enemyGameObject[buildingType].Add(building.transform);
            return;
        }
        _enemyGameObject.Add(buildingType, new List<Transform>());
        _enemyGameObject[buildingType].Add(building.transform);
    }

    public Transform GetPositionEnemyBuildersByType(BuildingType buildingType, Vector3 pos)
    {
        var findBuildings = new List<Transform>();
        foreach (var build in _enemyGameObject[buildingType])
        {
            findBuildings.Add(build);
        }

        if (findBuildings.Count == 0)
        {
            return null;
        }

        return Nearest(pos, findBuildings);
    }

    public Transform GetPositionEnemyBuilding(Vector3 pos)
    {
        var findBuildings = new List<Transform>();
        foreach (var builds in _enemyGameObject)
        {
            foreach (var build in builds.Value)
            {
                findBuildings.Add(build);
            }
        }

        if (findBuildings.Count == 0)
        {
            return null;
        }

        return Nearest(pos, findBuildings);
    }

    private Transform Nearest(Vector3 pos, List<Transform> findBuildings)
    {
        var closestDistance = Distance(pos, findBuildings[0].position);
        var closestTarget = findBuildings[0];

        for (int i = 1; i < findBuildings.Count; i++)
        {
            var distance = Distance(pos, findBuildings[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = findBuildings[i];
            }
        }

        return closestTarget;
    }

    private float Distance(Vector3 a, Vector3 b)
    {
        float num1 = a.x - b.x;
        float num2 = a.y - b.y;
        float num3 = a.z - b.z;
        return num1 * num1 + num2 * num2 + num3 * num3;
    }
}