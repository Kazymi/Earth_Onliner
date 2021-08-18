using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;

    private LauncherMenu _launcherMenu;

    private void Start()
    {
        _launcherMenu = ServiceLocator.GetService<LauncherMenu>();
        PhotonNetwork.ConnectUsingSettings();
    }

    // TODO: might be a good idea to invoke signals on photon events
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        _launcherMenu.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        var players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var player in players)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(player);
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        _launcherMenu.OnJoinedRoom(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _launcherMenu.OnJoinedRoomError(message);
    }

    public override void OnLeftRoom()
    {
        _launcherMenu.OnRoomLeft();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        foreach (var roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                continue;
            }

            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomInfo);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}