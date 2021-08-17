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

    private void Start()
    {
        ServiceLocator.GetService<Builders>().NewBuilding(gameObject,_isMine,buildingType);
        _photonView = GetComponent<PhotonView>();
    }
}