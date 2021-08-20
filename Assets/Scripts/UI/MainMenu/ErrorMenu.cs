using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMenu : MainMenuCanvas
{
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private Button toTitleButton;
    
    private Launcher _launcher;
    private MainMenuSystem _mainMenuSystem;

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
        _launcher.OnCreateRoomFailedAction -= OnCreateRoomFailed;
        toTitleButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        _mainMenuSystem = ServiceLocator.GetService<MainMenuSystem>();
        _launcher = ServiceLocator.GetService<Launcher>();
        Subscribe();
    }
    
    private void Subscribe()
    {
        toTitleButton.onClick.AddListener(() => _mainMenuSystem.OpenMenu(MainMenuCanvasType.Title));
        _launcher.OnCreateRoomFailedAction += OnCreateRoomFailed;
    }

    private void OnCreateRoomFailed(string errorText)
    {
        _mainMenuSystem.OpenMenu(MainMenuCanvasType.ErrorMenu);
    }
}
