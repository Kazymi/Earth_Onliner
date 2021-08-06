using System.IO;
using Photon.Pun;
using UnityEngine;

public class Earth : MonoBehaviour
{
    private void OnEnable()
    {
        ServiceLocator.Subscribe<Earth>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<Earth>();
    }

    [PunRPC]
    public void BuildNewBuilding(string nameBuilding, Vector3 localPos, Quaternion rotation)
    {
        var newBuild = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Builds", nameBuilding), Vector3.zero, Quaternion.identity);
        newBuild.transform.parent = transform;
        newBuild.transform.localPosition = localPos;
        newBuild.transform.localRotation = rotation;
    }
}