using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    [System.Serializable]
    public class TitleMenuView
    {
        [SerializeField] private Button toListRoomPanelButton;
        [SerializeField] private Button toCreateRoomPanelButton;
        [SerializeField] private Button exitGameButton;
        [SerializeField] TMP_InputField usernameInput;

        public Button ToListRoomPanelButton => toListRoomPanelButton;
        public Button ToCreateRoomPanelButton => toCreateRoomPanelButton;
        public Button ExitGameButton => exitGameButton;
        public TMP_InputField UsernameInput => usernameInput;
    }
}