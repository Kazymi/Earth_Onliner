using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MainMenuCanvas
{
    [SerializeField] private Button toListRoomPanelButton;
    [SerializeField] private Button toCreateRoomPanelButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] TMP_InputField usernameInput;
    

    private MainMenuSystem _mainMenuSystem;
    private Launcher _launcher;
    private PlayerNameSystem _playerNameSystem;

    private void Start()
    {
        _playerNameSystem = ServiceLocator.GetService<PlayerNameSystem>();
        _launcher = ServiceLocator.GetService<Launcher>();
        _mainMenuSystem = ServiceLocator.GetService<MainMenuSystem>();
        usernameInput.text = _playerNameSystem.CurrentNickName;
        AddListener();
    }

    private void OnEnable()
    {
        if (_mainMenuSystem == null || _launcher == null)
        {
            return;
        }
        AddListener();
    }

    private void OnDisable()
    {
        usernameInput.onValueChanged.RemoveListener(UpdateNickName);
        toListRoomPanelButton.onClick.RemoveAllListeners();
        toCreateRoomPanelButton.onClick.RemoveAllListeners();
        exitGameButton.onClick.RemoveAllListeners();
        _launcher.OnJoinedLobbyAction -= OnJoinedLobby;
    }

    private void UpdateNickName(string newText)
    {
        _playerNameSystem.UpdateNickName(newText);
    }
    private void AddListener()
    {
        usernameInput.onValueChanged.AddListener(UpdateNickName);
        toListRoomPanelButton.onClick.AddListener(() => _mainMenuSystem.OpenMenu(MainMenuCanvasType.FindRoom));
        toCreateRoomPanelButton.onClick.AddListener(() => _mainMenuSystem.OpenMenu(MainMenuCanvasType.CreateGame));
        exitGameButton.onClick.AddListener(Application.Quit);
        _launcher.OnJoinedLobbyAction += OnJoinedLobby;
    }
    
    private void OnJoinedLobby()
    {
        _mainMenuSystem.OpenMenu(MainMenuCanvasType.Title);
    }
}