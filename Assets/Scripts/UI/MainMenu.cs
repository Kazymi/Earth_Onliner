using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button toListRoomPanelButton;
    [SerializeField] private Button toCreateRoomPanelButton;
    [SerializeField] private List<Button> toTitlePanelButtons;
    [SerializeField] private Button exitGameButton;

    private MainMenuSystem _mainMenuSystem;

    private void Start()
    {
        _mainMenuSystem = ServiceLocator.GetService<MainMenuSystem>();
        AddListener();
    }

    private void OnEnable()
    {
        if (_mainMenuSystem == null)
        {
            return;
        }
        AddListener();
    }

    private void OnDisable()
    {
        toListRoomPanelButton.onClick.RemoveAllListeners();
        toCreateRoomPanelButton.onClick.RemoveAllListeners();
        exitGameButton.onClick.RemoveAllListeners();
        foreach (var button in toTitlePanelButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void AddListener()
    {
        toListRoomPanelButton.onClick.AddListener(() => _mainMenuSystem.OpenMenu(MainMenuCanvasType.FindRoom));
        toCreateRoomPanelButton.onClick.AddListener(() => _mainMenuSystem.OpenMenu(MainMenuCanvasType.CreateGame));
        exitGameButton.onClick.AddListener(Application.Quit);
        foreach (var button in toTitlePanelButtons)
        {
            button.onClick.AddListener(() => _mainMenuSystem.OpenMenu(MainMenuCanvasType.Title));
        }
    }
}