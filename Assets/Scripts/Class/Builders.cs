using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builders
{
    private List<Transform> _mineGameObject = new List<Transform>();
    private List<Transform> _enemyGameObject = new List<Transform>();

    public void NewBuilding(GameObject building, bool isMine)
    {
        // if (!isMine)
        // {
        //     _mineGameObject.Add(building.transform);
        // }
        // else
        // {
        //     _enemyGameObject.Add(building.transform);
        // }
        _enemyGameObject.Add(building.transform);
    }

    public List<Transform> GetEnemyBuildersPosition()
    {
        return _enemyGameObject;
    }
}
