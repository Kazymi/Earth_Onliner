using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArmySystemMenu : MonoBehaviour
{
    [SerializeField] private Transform armyTransform;
    [SerializeField] private ArmyPanel armyPanelPrefab;
    [SerializeField] private Button openArmyButton;
    [SerializeField] private Button closeArmyButton;
    [SerializeField] private Canvas armyCanvas;

    private ArmySystem _armySystem;

    private void OnEnable()
    {
        openArmyButton.onClick.AddListener(OpenCanvas);
        closeArmyButton.onClick.AddListener(CloseCanvas);
    }

    private void OnDisable()
    {
        openArmyButton.onClick.RemoveListener(OpenCanvas);
        closeArmyButton.onClick.RemoveListener(CloseCanvas);
    }

    private void Start()
    {
        _armySystem = ServiceLocator.GetService<ArmySystem>();
        _armySystem.Initialize(armyTransform, armyPanelPrefab);
    }

    private void OpenCanvas()
    {
        openArmyButton.interactable = false;
        armyCanvas.enabled = true;
    }

    private void CloseCanvas()
    {
        openArmyButton.interactable = true;
        armyCanvas.enabled = false;
    }
}