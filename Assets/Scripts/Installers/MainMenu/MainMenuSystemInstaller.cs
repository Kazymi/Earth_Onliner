using System.Collections.Generic;
using UnityEngine;

public class MainMenuSystemInstaller : MonoBehaviour
{
    [SerializeField] private List<MainMenuCanvas> mainMenuCanvasList;
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomListItemPrefab;

    private void Awake()
    {
        ServiceLocator.Subscribe<Launcher>(new Launcher());
        ServiceLocator.Subscribe<PlayerNameSystem>(new PlayerNameSystem());
        ServiceLocator.Subscribe<ListRoomSystem>(new ListRoomSystem(roomListContent, roomListItemPrefab));
        ServiceLocator.Subscribe<MainMenuSystem>(new MainMenuSystem(mainMenuCanvasList));
        ServiceLocator.Subscribe<LauncherSystem>(new LauncherSystem());
        ServiceLocator.Subscribe<RoomSystem>(new RoomSystem());
    }

    private void OnDestroy()
    {
        ServiceLocator.GetService<Launcher>().Dispose();
    }
}