using TMPro;
using UnityEngine;

public class LauncherMenu : MonoBehaviour
{
    private MainMenuSystem _menuManager;
    private Launcher _launcher;
    private void Start()
    {
        _menuManager = ServiceLocator.GetService<MainMenuSystem>();
        _launcher = ServiceLocator.GetService<Launcher>();
        Subscribe();
    }

    private void OnEnable()
    {
        if (_launcher == null)
        {
            return;
        }
        Subscribe();
    }

    private void OnDisable()
    {
        _launcher.OnJoinedLobbyAction -= OnJoinedLobby;
        _launcher.OnJoinedRoomAction -= OnJoinedRoom;
    }

    private void Subscribe()
    {
        _launcher.OnJoinedLobbyAction += OnJoinedLobby;
        _launcher.OnJoinedRoomAction += OnJoinedRoom;
    }

    private void OnJoinedLobby()
    {
        _menuManager.OpenMenu(MainMenuCanvasType.Title);
    }

    private void OnJoinedRoom()
    {
        _menuManager.OpenMenu(MainMenuCanvasType.RoomMenu);
    }
}