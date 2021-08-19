
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainMenuSystemInstaller : MonoBehaviour
    {
        [SerializeField] private List<MainMenuCanvas> mainMenuCanvasList;
        [SerializeField] private Launcher launcher;
        [SerializeField] private Transform roomListContent;
        [SerializeField] private GameObject roomListItemPrefab;

        private PlayerNameSystem _playerNameSystem;
        private RoomSystem _roomSystem;
        private MainMenuSystem _mainMenuSystem;
        private LauncherSystem _launcherSystem;
        private ListRoomSystem _listRoomSystem;
        
        private void Awake()
        {
            ServiceLocator.Subscribe<Launcher>(launcher);

            _playerNameSystem = new PlayerNameSystem();
            ServiceLocator.Subscribe<PlayerNameSystem>(_playerNameSystem);
            
            _listRoomSystem = new ListRoomSystem(roomListContent, roomListItemPrefab);
            ServiceLocator.Subscribe<ListRoomSystem>(_listRoomSystem);
            
            _mainMenuSystem = new MainMenuSystem(mainMenuCanvasList);
            ServiceLocator.Subscribe<MainMenuSystem>(_mainMenuSystem);

            _launcherSystem = new LauncherSystem();
            ServiceLocator.Subscribe<LauncherSystem>(_launcherSystem);

            _roomSystem = new RoomSystem();
            ServiceLocator.Subscribe<RoomSystem>(_roomSystem);
        }
    }