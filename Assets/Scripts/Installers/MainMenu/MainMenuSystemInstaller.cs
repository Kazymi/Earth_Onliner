
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainMenuSystemInstaller : MonoBehaviour
    {
        [SerializeField] private List<MainMenuCanvas> mainMenuCanvasList;
        [SerializeField] private Launcher launcher;

        private MainMenuSystem _mainMenuSystem;
        private LauncherSystem _launcherSystem;

        private void Awake()
        {
            _mainMenuSystem = new MainMenuSystem(mainMenuCanvasList);
            ServiceLocator.Subscribe<MainMenuSystem>(_mainMenuSystem);

            _launcherSystem = new LauncherSystem();
            ServiceLocator.Subscribe<LauncherSystem>(_launcherSystem);
            
            ServiceLocator.Subscribe<Launcher>(launcher);
        }
    }