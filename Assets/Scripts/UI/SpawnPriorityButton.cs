using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpawnPriorityButton : MonoBehaviour
{
    [SerializeField] private BuildingType _buildingType;
    
    private Button _button;
    private SpawnArmySystemMenu _spawnArmySystemMenu;
    private SpawnArmySystem _spawnArmySystem;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _spawnArmySystem = ServiceLocator.GetService<SpawnArmySystem>();
        _spawnArmySystemMenu = ServiceLocator.GetService<SpawnArmySystemMenu>();
    }

    private void OnEnable()
    {
        if (_button == null)
        {
            _button = GetComponent<Button>();
        }
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _spawnArmySystemMenu.ActivateAllPriorityButton();
        _button.interactable = false;
        _spawnArmySystem.UpdatePriority(_buildingType);
        _spawnArmySystemMenu.UpdateSpawnButton();
    }
}