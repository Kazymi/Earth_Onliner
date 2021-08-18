﻿using Photon.Pun;
using Photon.Realtime;

public class LauncherSystem
{
    private MainMenuSystem _menuManager;

    public LauncherSystem()
    {
        _menuManager = ServiceLocator.GetService<MainMenuSystem>();
    }
    
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        _menuManager.OpenMenu(MainMenuCanvasType.Load);
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        _menuManager.OpenMenu(MainMenuCanvasType.Load);
    }
    
    public void CreateRoom(string nameRoom)
    {
        PhotonNetwork.CreateRoom(nameRoom);
        _menuManager.OpenMenu(MainMenuCanvasType.Load);
    }
}