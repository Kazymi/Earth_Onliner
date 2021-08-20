using System;
using System.Collections.Generic;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : PunCallbacks
{
    public event Action OnConnectedToMasterAction;
    public event Action OnJoinedLobbyAction;
    public event Action OnJoinedRoomAction;
    public event Action<Player> OnMasterClientSwitchedAction;
    public event Action<Player> OnNewPlayerJoinedRoomAction;
    public event Action<List<RoomInfo>> OnRoomListUpdateAction;
    public event Action<string> OnCreateRoomFailedAction;
    public event Action OnLeftRoomAction;

    public Launcher()
    {
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        OnConnectedToMasterAction?.Invoke();
    }

    public override void OnJoinedLobby()
    {
        OnJoinedLobbyAction?.Invoke();
    }

    public override void OnJoinedRoom()
    {
        OnJoinedRoomAction?.Invoke();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        OnMasterClientSwitchedAction?.Invoke(newMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        OnCreateRoomFailedAction?.Invoke(message);
    }

    public override void OnLeftRoom()
    {
        OnLeftRoomAction?.Invoke();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        OnRoomListUpdateAction?.Invoke(roomList);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        OnNewPlayerJoinedRoomAction?.Invoke(newPlayer);
    }

    public void Dispose()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}