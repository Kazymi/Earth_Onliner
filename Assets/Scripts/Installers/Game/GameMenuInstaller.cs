
    using System;
    using UnityEngine;

    public class GameMenuInstaller : MonoBehaviour
    {
        [SerializeField] private UpgradeSystemMenu upgradeSystemMenu;
        [SerializeField] private BuildSystemMenu buildSystemMenu;
        [SerializeField] private GameMenu gameMenu;
        [SerializeField] private SpawnArmySystemMenu spawnArmySystemMenu;

        private void Awake()
        {
            ServiceLocator.Subscribe<SpawnArmySystemMenu>(spawnArmySystemMenu);
            ServiceLocator.Subscribe<GameMenu>(gameMenu);
            ServiceLocator.Subscribe<BuildSystemMenu>(buildSystemMenu);
            ServiceLocator.Subscribe<UpgradeSystemMenu>(upgradeSystemMenu);
        }
    }