using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomCreateMenu : MainMenuCanvas
{
    [SerializeField] private Button createGameButton;
    [SerializeField] TMP_InputField roomNameInputField;

    private LauncherSystem _launcherSystem;
    private void OnEnable()
    {
        createGameButton.onClick.AddListener(CreateRoom);
    }

    private void OnDisable()
    {
        createGameButton.onClick.RemoveListener(CreateRoom);
    }

    private void Start()
    {
        _launcherSystem = ServiceLocator.GetService<LauncherSystem>();
    }

    private void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }

        _launcherSystem.CreateRoom(roomNameInputField.text);
    }
}