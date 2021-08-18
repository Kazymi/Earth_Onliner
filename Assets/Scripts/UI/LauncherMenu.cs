using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LauncherMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;

    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button leaveRoomButton;
    [SerializeField] private Button startGameRoomButton;

    private MainMenuSystem _menuManager;
    private LauncherSystem _launcherSystem;

    private void Start()
    {
        _menuManager = ServiceLocator.GetService<MainMenuSystem>();
        _launcherSystem = ServiceLocator.GetService<LauncherSystem>();
        leaveRoomButton.onClick.AddListener(_launcherSystem.LeaveRoom);
    }

    private void OnEnable()
    {
        createRoomButton.onClick.AddListener(CreateRoom);
        startGameRoomButton.onClick.AddListener(StartGame);
        if (_launcherSystem == null)
        {
            return;
        }
        leaveRoomButton.onClick.AddListener(_launcherSystem.LeaveRoom);
    }

    private void OnDisable()
    {
        createRoomButton.onClick.RemoveListener(CreateRoom);
        leaveRoomButton.onClick.RemoveListener(_launcherSystem.LeaveRoom);
        startGameRoomButton.onClick.RemoveListener(StartGame);
    }

    private void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        _launcherSystem.CreateRoom(roomNameInputField.text);
    }

    public void OnJoinedLobby()
    {
        _menuManager.OpenMenu(MainMenuCanvasType.Title);
    }

    public void OnJoinedRoom(string nameRoom)
    {
        _menuManager.OpenMenu(MainMenuCanvasType.RoomMenu);
        roomNameText.text = nameRoom;
    }

    public void OnJoinedRoomError(string nameError)
    {
        errorText.text = "Room Creation Failed: " + nameError;
        Debug.LogError("Room Creation Failed: " + nameError);
        _menuManager.OpenMenu(MainMenuCanvasType.ErrorMenu);
    }

    public void OnRoomLeft()
    {
        _menuManager.OpenMenu(MainMenuCanvasType.Title);
    }

    private void StartGame()
    {
        _launcherSystem.StartGame();
        startGameRoomButton.interactable = false;
    }
}