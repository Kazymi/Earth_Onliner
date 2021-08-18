using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSystem
{
    private List<MainMenuCanvas> _menus;
    private Dictionary<MainMenuCanvasType, List<Canvas>> _mainMenuCanvases =
        new Dictionary<MainMenuCanvasType, List<Canvas>>();

    
    public MainMenuSystem(List<MainMenuCanvas> menu)
    {
        _menus = menu;
        foreach (var mainMenuCanvas in _menus)
        {
            var type = mainMenuCanvas.MainMenuCanvasType;
            if (_mainMenuCanvases.ContainsKey(type) == false)
            {
                _mainMenuCanvases.Add(type, new List<Canvas>());
            }

            _mainMenuCanvases[type].Add(mainMenuCanvas.GetComponent<Canvas>());
        }
    }

    public void OpenMenu(MainMenuCanvasType mainMenuCanvasType)
    {
        if (_mainMenuCanvases.ContainsKey(mainMenuCanvasType) == false)
        {
            Debug.LogError($"Canvas {mainMenuCanvasType} not found");
            return;
        }
        DisableAllCanvas();
        foreach (var canvas in _mainMenuCanvases[mainMenuCanvasType])
        {
            canvas.enabled = true;
        }
    }

    private void DisableAllCanvas()
    {
        foreach (var menuCanvases in _mainMenuCanvases)
        {
            foreach (var canvas in menuCanvases.Value)
            {
                canvas.enabled = false;
            }
        }
    }
}