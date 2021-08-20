using UI.MainMenu;
using UnityEngine;

public class TitleMenu : MainMenuCanvas
{
    [SerializeField] TitleMenuView titleMenuView;

    private MainMenuSystem _mainMenuSystem;
    private Launcher _launcher;
    private PlayerNameSystem _playerNameSystem;

    private void Start()
    {
        titleMenuView.UsernameInput.text = _playerNameSystem.CurrentNickName;
    }

    private void OnEnable()
    {
        ResolveDependencies();

        AddListener();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void UpdateNickName(string newText)
    {
        _playerNameSystem.UpdateNickName(newText);
    }

    private void AddListener()
    {
        titleMenuView.UsernameInput.onValueChanged.AddListener(UpdateNickName);
        titleMenuView.ToListRoomPanelButton.onClick.AddListener(() => _mainMenuSystem.OpenMenu(MainMenuCanvasType.FindRoom));
        titleMenuView.ToCreateRoomPanelButton.onClick.AddListener(() => _mainMenuSystem.OpenMenu(MainMenuCanvasType.CreateGame));
        titleMenuView.ExitGameButton.onClick.AddListener(Application.Quit);
        _launcher.OnJoinedLobbyAction += OnJoinedLobby;
    }

    private void RemoveListeners()
    {
        titleMenuView.UsernameInput.onValueChanged.RemoveListener(UpdateNickName);
        titleMenuView.ToListRoomPanelButton.onClick.RemoveAllListeners();
        titleMenuView.ToCreateRoomPanelButton.onClick.RemoveAllListeners();
        titleMenuView.ExitGameButton.onClick.RemoveAllListeners();
        _launcher.OnJoinedLobbyAction -= OnJoinedLobby;
    }

    private void OnJoinedLobby()
    {
        _mainMenuSystem.OpenMenu(MainMenuCanvasType.Title);
    }

    private void ResolveDependencies()
    {
        _playerNameSystem = ServiceLocator.GetService<PlayerNameSystem>();
        _launcher = ServiceLocator.GetService<Launcher>();
        _mainMenuSystem = ServiceLocator.GetService<MainMenuSystem>();
    }
}