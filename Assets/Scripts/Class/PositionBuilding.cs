using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBuilding
{
    private Vector3 _position;
    private Vector3 _normal;

    public Vector3 Position => _position;
    public Vector3 Normal => _normal;

    public PositionBuilding(Vector3 position, Vector3 normal)
    {
        this._position = position;
        this._normal = normal;
    }
}
