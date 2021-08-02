using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavMeshSphere))]
public class NavMeshSphereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Load navmesh data"))
        {
            (target as NavMeshSphere).LoadNavmeshData();
        }

        if (GUILayout.Button("remove navmesh data"))
        {
            (target as NavMeshSphere).RemoveAllNavMeshLoadedData();
        }
    }
}
