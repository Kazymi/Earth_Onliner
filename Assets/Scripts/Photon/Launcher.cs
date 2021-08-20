﻿using System;
using System.Collections.Generic;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : PunCallbacks
{
    private Player _newPlayer;
    private List<RoomInfo> _newRoom;
    private string _errorText;

    public event Action OnConnectedToMasterAction;
    public event Action OnJoinedLobbyAction;
    public event Action OnJoinedRoomAction;
    public event Action<Player> OnMasterClientSwitchedAction;
    public event Action OnNewPlayerJoinedRoomAction;
    public event Action OnRoomListUpdateAction;
    public event Action OnCreateRoomFailedAction;
    public event Action OnLeftRoomAction;

    public Player NewPlayer => _newPlayer;
    public List<RoomInfo> NewRoom => _newRoom;
    public string ErrorText => _errorText;

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
        _errorText = message;
        OnCreateRoomFailedAction?.Invoke();
    }

    public override void OnLeftRoom()
    {
        OnLeftRoomAction?.Invoke();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // TODO:
        _newRoom = roomList;
        OnRoomListUpdateAction?.Invoke();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // TODO:
        _newPlayer = newPlayer;
        OnNewPlayerJoinedRoomAction?.Invoke();
    }

    public void Dispose()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}