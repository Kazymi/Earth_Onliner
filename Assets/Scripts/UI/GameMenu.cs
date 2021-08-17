using System.Collections.Generic;
using EventBusSystem;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour, IChangingAmountResources
{
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text ironText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private Canvas exitGameCanvas;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button disconnectGameButton;
    [SerializeField] private Button returnToGameButton;

    private Dictionary<TypeResource, TMP_Text> _changingResources;

    private void Start()
    {
        _changingResources = new Dictionary<TypeResource, TMP_Text>();

        _changingResources.Add(TypeResource.Wood,
            woodText);

        _changingResources.Add(TypeResource.Iron,
            ironText);

        _changingResources.Add(TypeResource.Gold,
            goldText);
    }

    private void OnEnable()
    {
        ServiceLocator.Subscribe<GameMenu>(this);
        EventBus.Subscribe(this);
        
        disconnectGameButton.onClick.AddListener(Disconnect);
        returnToGameButton.onClick.AddListener(() => OpenCanvas(false));
        exitGameButton.onClick.AddListener(() => OpenCanvas(true));
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<GameMenu>();
        EventBus.Unsubscribe(this);
        
        disconnectGameButton.onClick.RemoveAllListeners();
        returnToGameButton.onClick.RemoveAllListeners();
        exitGameButton.onClick.RemoveAllListeners();
    }

    public void ChangingAmountResources(TypeResource typeResource, int amount)
    {
        _changingResources[typeResource].text = amount.ToString();
    }

    private void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    private void OpenCanvas(bool opened)
    {
        exitGameCanvas.enabled = opened;
        exitGameButton.interactable = !opened;
    }
}