using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomMenu : MainMenuCanvas
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button leaveRoomButton;
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerListItemPrefab;

    private RoomSystem _roomSystem;
    private LauncherSystem _launcherSystem;
    private Launcher _launcher;
    private MainMenuSystem _mainMenuSystem;

    private void OnEnable()
    {
        if (_launcher == null)
        {
            return;
        }

        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Start()
    {
        ResolveDependencies();
        Subscribe();
    }

    private void StartGame()
    {
        _launcherSystem.StartGame();
        startGameButton.interactable = false;
    }

    private void Unsubscribe()
    {
        _launcher.OnJoinedRoomAction -= OnJoinedRoom;
        _launcher.OnMasterClientSwitchedAction -= OnMasterClientSwitched;
        _launcher.OnNewPlayerJoinedRoomAction -= OnPlayerEnteredRoom;
        _launcher.OnLeftRoomAction -= OnPlayerLeaveRoom;
        _launcher.OnJoinedRoomAction -= OnCreateRoom;
        leaveRoomButton.onClick.RemoveListener(_launcherSystem.LeaveRoom);
        startGameButton.onClick.RemoveListener(StartGame);
    }
    private void Subscribe()
    {
        _launcher.OnJoinedRoomAction += OnJoinedRoom;
        _launcher.OnMasterClientSwitchedAction += OnMasterClientSwitched;
        _launcher.OnNewPlayerJoinedRoomAction += OnPlayerEnteredRoom;
        _launcher.OnLeftRoomAction += OnPlayerLeaveRoom;
        _launcher.OnJoinedRoomAction += OnCreateRoom;
        leaveRoomButton.onClick.AddListener(_launcherSystem.LeaveRoom);
        startGameButton.onClick.AddListener(StartGame);
    }

    private void OnCreateRoom()
    {
       _mainMenuSystem.OpenMenu(MainMenuCanvasType.RoomMenu);
    }
    private void OnPlayerEnteredRoom(Player player)
    {
    _roomSystem.NewPlayer(player,playerListContent,playerListItemPrefab);    
    }

    private void OnMasterClientSwitched(Player player)
    {
        startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    private void OnJoinedRoom()
    {
        startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        _roomSystem.PlayerJoinedRoom(playerListContent, playerListItemPrefab);
    }

    private void OnPlayerLeaveRoom()
    {
        _mainMenuSystem.OpenMenu(MainMenuCanvasType.Title);
    }

    private void ResolveDependencies()
    {
        _mainMenuSystem = ServiceLocator.GetService<MainMenuSystem>();
        _roomSystem = ServiceLocator.GetService<RoomSystem>();
        _launcher = ServiceLocator.GetService<Launcher>();
        _launcherSystem = ServiceLocator.GetService<LauncherSystem>();
    }
}