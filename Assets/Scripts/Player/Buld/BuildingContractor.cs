using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class BuildingContractor : MonoBehaviour
{
    [SerializeField] private float radiusBuilding;
    [SerializeField] private BuildingType buildingType;
    [SerializeField] private BuildingResource resourcesForBuilding;
    [SerializeField] private NPCHealth _health;
    
    private bool _isMine;
    private PhotonView _photonView;
    
    public bool IsMine => _isMine;
    public PhotonView PhotonView => _photonView;
    public void BuildComplete(bool isMine)
    {
        _isMine = isMine;
        _health.IsMine = isMine;
    }
    public BuildingResource Resource => resourcesForBuilding;
    public float RadiusBuilding => radiusBuilding;

    private void Start()
    {
        ServiceLocator.GetService<Builders>().NewBuilding(gameObject,_isMine,buildingType);
        _photonView = GetComponent<PhotonView>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(radiusBuilding, 1, radiusBuilding));
    }
}