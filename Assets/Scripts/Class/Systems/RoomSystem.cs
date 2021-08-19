using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomSystem
{
    public void PlayerJoinedRoom(Transform playerListContent, GameObject playerListItemPrefab)
    {
        var players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var player in players)
        {
            GameObject.Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(player);
        }
    }

    public void NewPlayer(Player player,Transform playerListContent, GameObject playerListItemPrefab)
    {
        GameObject.Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(player);

    }

}