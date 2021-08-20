using System.Collections.Generic;
using DG.Tweening;
using EventBusSystem;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour, IChangingAmountResources, IOnEventCallback
{
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text ironText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private Canvas exitGameCanvas;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button disconnectGameButton;
    [SerializeField] private Button returnToGameButton;
    [SerializeField] private TMP_Text finishGameText;

    private bool _gameFinished;
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
        EventBus.Subscribe(this);
        PhotonNetwork.AddCallbackTarget(this);
        disconnectGameButton.onClick.AddListener(Disconnect);
        returnToGameButton.onClick.AddListener(() => OpenCanvas(false));
        exitGameButton.onClick.AddListener(() => OpenCanvas(true));
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);

        PhotonNetwork.RemoveCallbackTarget(this);
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
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    private void OpenCanvas(bool opened)
    {
        exitGameCanvas.enabled = opened;
        exitGameButton.interactable = !opened;
    }

    public void OnEvent(EventData photonEvent)
    {
        if (_gameFinished)
        {
            return;
        }

        var eventCode = photonEvent.Code;

        if (eventCode != (int)EventType.FinishGame)
        {
            return;
        }

        object[] data = (object[])photonEvent.CustomData;
        _gameFinished = true;
        finishGameText.transform.DOScale(2, 3);
        finishGameText.text = (string)data[0] + " WIN";
    }
}