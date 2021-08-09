using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class BuildingContractor : MonoBehaviour
{
    [SerializeField] private float radiusBuilding;
    [SerializeField] private BuildingResource resourcesForBuilding;
    
    private bool _isMine;

    public bool IsMine => _isMine;
    public void BuildComplete(bool isMine)
    {
        _isMine = isMine;
        ServiceLocator.GetService<Builders>().NewBuilding(gameObject,isMine);
    }
    public BuildingResource Resource => resourcesForBuilding;
    public float RadiusBuilding => radiusBuilding;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(radiusBuilding, 1, radiusBuilding));
    }
}