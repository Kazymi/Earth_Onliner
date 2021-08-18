using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildSystemMenu : MonoBehaviour
{
    [SerializeField] private Button buildButton;
    [SerializeField] private Button rotateButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Canvas canvas;

    private BuildSystem _buildSystem;
    private void OnEnable()
    {
        if (_buildSystem == null)
        {
            return;
        }
        rotateButton.onClick.AddListener(_buildSystem.Rotate);
        exitButton.onClick.AddListener(_buildSystem.Exit);
    }

    private void OnDisable()
    {
        rotateButton.onClick.RemoveListener(_buildSystem.Rotate);
        exitButton.onClick.RemoveListener(_buildSystem.Exit);
    }

    private void Start()
    {
        _buildSystem = ServiceLocator.GetService<BuildSystem>();
        rotateButton.onClick.AddListener(_buildSystem.Rotate);
        exitButton.onClick.AddListener(_buildSystem.Exit);
    }

    public void ActivateCanvas(bool activated)
    {
        canvas.enabled = activated;
    }
    
    public void UnlockBuildButton(bool unlock)
    {
        buildButton.interactable = unlock;
    }
}