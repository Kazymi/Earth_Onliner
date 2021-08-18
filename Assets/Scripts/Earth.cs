using System.IO;
using Photon.Pun;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [PunRPC]
    public void BuildNewBuilding(string nameBuilding, Vector3 localPos, Quaternion rotation, string namePlayer)
    {
        var isMine = PhotonNetwork.LocalPlayer.UserId == namePlayer;
        if (isMine)
        {
            var newBuild = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Builds", nameBuilding),
                Vector3.zero, Quaternion.identity);
            newBuild.transform.parent = transform;
            newBuild.transform.localPosition = localPos;
            newBuild.transform.localRotation = rotation;
            newBuild.GetComponent<BuildingContractor>().BuildComplete(isMine);
        }
    }
}