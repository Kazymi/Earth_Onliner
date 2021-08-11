using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnArmySystemMenu : MonoBehaviour
{
    [SerializeField] private Transform armyPanel;
    [SerializeField] private ArmyPanel armyPanelPrefab;
    [SerializeField] private List<Button> amountButtons;
    [SerializeField] private List<Button> buildingButtons;
    [SerializeField] private Button autoSpawnButton;
    [SerializeField] private Button spawnButton;
    [SerializeField] private Canvas spawnArmySystemCanvas;
    [SerializeField] private Button closeButton;

    private SpawnArmySystem _spawnArmySystem;

    private void Start()
    {
        _spawnArmySystem = ServiceLocator.GetService<SpawnArmySystem>();
    }

    private void OnEnable()
    {
        ServiceLocator.Subscribe<SpawnArmySystemMenu>(this);
        autoSpawnButton.onClick.AddListener(AutoSpawn);
        spawnButton.onClick.AddListener(Spawn);
        closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<SpawnArmySystemMenu>();
        autoSpawnButton.onClick.RemoveListener(AutoSpawn);
        spawnButton.onClick.RemoveListener(Spawn);
        closeButton.onClick.RemoveListener(Close);
    }

    public void ActivateAllAmountButton()
    {
        foreach (var button in amountButtons)
        {
            button.interactable = true;
        }
    }

    public void ActivateAllPriorityButton()
    {
        foreach (var button in buildingButtons)
        {
            button.interactable = true;
        }
    }

    public void Initialize(List<NPCConfiguration> npcConfigurations, ArmySpawner armySpawner)
    {
        spawnArmySystemCanvas.enabled = true;
        ActivateAllAmountButton();
        ActivateAllPriorityButton();
        autoSpawnButton.interactable = false;
        spawnButton.interactable = false;
        _spawnArmySystem.Initialize(npcConfigurations, armyPanel, armyPanelPrefab, armySpawner);
    }

    public void UpdateSpawnButton()
    {
        if (_spawnArmySystem.AllComponentSelected)
        {
            spawnButton.interactable = true;
            autoSpawnButton.interactable = true;
            _spawnArmySystem.UpdateAutoSpawn(false);
        }
    }

    private void AutoSpawn()
    {
        autoSpawnButton.interactable = false;
        _spawnArmySystem.UpdateAutoSpawn(true);
        _spawnArmySystem.AutoSpawn();
    }

    private void Spawn()
    {
        autoSpawnButton.interactable = true;
        _spawnArmySystem.UpdateAutoSpawn(false);
        _spawnArmySystem.Spawn();
    }

    private void Close()
    {
        spawnArmySystemCanvas.enabled = false;
    }
}

public class SpawnArmySystem
{
    private ArmySpawnConfiguration _spawnConfiguration;
    private ArmySystem _armySystem;
    private ArmySpawner _armySpawner;
    private bool _selectedType;
    private bool _selectedAmount;

    public bool AllComponentSelected => _selectedType && _selectedAmount;

    public ArmySpawnConfiguration ArmySpawnConfiguration => _spawnConfiguration;

    public SpawnArmySystem()
    {
        _spawnConfiguration = new ArmySpawnConfiguration();
    }

    public void Initialize(List<NPCConfiguration> npcConfigurations, Transform parent, ArmyPanel armyPanel,
        ArmySpawner armySpawner)
    {
        _selectedAmount = false;
        _selectedType = false;
        _armySpawner = armySpawner;
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject.Destroy(parent.GetChild(i).gameObject);
        }

        _armySystem = ServiceLocator.GetService<ArmySystem>();
        foreach (var config in npcConfigurations)
        {
            var newArmyPanel = GameObject.Instantiate(armyPanel, parent);

            newArmyPanel.Initialize(config.NameNpcInArmy);
            newArmyPanel.UpdateState(_armySystem.AmountArmy[config]);
        }
    }

    public void UpdateAmountEnemy(int amount)
    {
        _selectedAmount = true;
        _spawnConfiguration.AmountEnemyOnSpawn = amount;
    }

    public void UpdatePriority(BuildingType buildingType)
    {
        _selectedType = true;
        _spawnConfiguration.BuildingType = buildingType;
    }

    public void UpdateAutoSpawn(bool isAutoSpawn)
    {
        _spawnConfiguration.IsAutoSpawn = isAutoSpawn;
    }

    public void Spawn()
    {
        _armySpawner.Initialize(ArmySpawnConfiguration);
        _armySpawner.StartSpawn();
    }

    public void AutoSpawn()
    {
        _armySpawner.Initialize(ArmySpawnConfiguration);
    }
}