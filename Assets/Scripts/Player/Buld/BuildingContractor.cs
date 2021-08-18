using System;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class BuildingContractor : MonoBehaviour
{
    [SerializeField] private NPCHealth _health;
    [SerializeField] private BuildingType buildingType;
    
    private bool _isMine;
    private PhotonView _photonView;

    public bool IsMine => _isMine;
    public PhotonView PhotonView => _photonView;
    public void BuildComplete(bool isMine)
    {
        _isMine = isMine;
        _health.IsMine = isMine;
    }

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        var builders = ServiceLocator.GetService<Builders>();
        Debug.Log(builders);
        builders.NewBuilding(gameObject,_isMine,buildingType);
    }
}