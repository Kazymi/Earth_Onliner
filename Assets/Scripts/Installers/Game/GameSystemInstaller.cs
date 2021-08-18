using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemInstaller : MonoBehaviour
{
    [SerializeField] private PlayerSystem playerSystem;
    [SerializeField] private ArmySystem armySystem;
    
    private BuildSystem _buildSystem;
    private SpawnArmySystem _spawnArmySystem;

    private void Awake()
    {
        _buildSystem = new BuildSystem();
        _spawnArmySystem = new SpawnArmySystem();
        
        ServiceLocator.Subscribe<ArmySystem>(armySystem);
        ServiceLocator.Subscribe<SpawnArmySystem>(_spawnArmySystem);
        ServiceLocator.Subscribe<BuildSystem>(_buildSystem);
        ServiceLocator.Subscribe<PlayerSystem>(playerSystem);
    }
}
