using UnityEngine;

    public abstract class MainMenuCanvas : MonoBehaviour
    {
        [SerializeField] private MainMenuCanvasType mainMenuCanvasType;

        public MainMenuCanvasType MainMenuCanvasType => mainMenuCanvasType;
    }