using UnityEngine;

public struct PositionBuilding
{
    public Vector3 Position { get; }
    public Vector3 Normal { get; }

    public PositionBuilding(Vector3 position, Vector3 normal)
    {
        Position = position;
        Normal = normal;
    }
}