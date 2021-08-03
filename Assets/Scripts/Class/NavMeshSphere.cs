using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

[Serializable]
public struct NavMeshChunk
{
    public Vector3 EulerRotation;
    public NavMeshData Data;            
    public bool Enabled;
}


public class NavMeshSphere : MonoBehaviour
{
    [SerializeField]
    private List<NavMeshChunk> _navMeshChunks;
    
    private void Start()
    {
        foreach (var chunk in _navMeshChunks)
        {
            var newGameObject = new GameObject();
            newGameObject.transform.parent = transform;
            newGameObject.AddComponent(typeof(NavMeshSurface));
            newGameObject.transform.rotation = Quaternion.Euler(chunk.EulerRotation);
            var newNav = newGameObject.GetComponent<NavMeshSurface>();
            newNav.navMeshData = chunk.Data;
            newNav.AddData();   
        }
    }
}
