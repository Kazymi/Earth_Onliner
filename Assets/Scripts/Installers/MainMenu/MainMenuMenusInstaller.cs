
    using System;
    using UnityEngine;

    public class MainMenuMenusInstaller: MonoBehaviour
    {
        [SerializeField] private LauncherMenu launcherMenu;

        private void Awake()
        {
            ServiceLocator.Subscribe<LauncherMenu>(launcherMenu);
        }
    }
