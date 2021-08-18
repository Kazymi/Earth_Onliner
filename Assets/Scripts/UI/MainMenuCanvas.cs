using UnityEngine;

    public class MainMenuCanvas : MonoBehaviour
    {
        [SerializeField] private MainMenuCanvasType mainMenuCanvasType;

        public MainMenuCanvasType MainMenuCanvasType => mainMenuCanvasType;
    }