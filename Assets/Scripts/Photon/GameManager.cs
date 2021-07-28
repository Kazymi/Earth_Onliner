using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PhotonView))]
public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private int needToFindKeys = 3;

    private bool _gameFinished;

    public override void OnEnable()
    {
        ServiceLocator.Subscribe<GameManager>(this);
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        ServiceLocator.Unsubscribe<GameManager>();
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (_gameFinished)
        {
            return;
        }
        
        var eventCode = photonEvent.Code;
        
        if (eventCode != (int) EventType.FinishGame)
        {
            return;
        }
        PlayerWin();
    }
    
    private void PlayerWin()
    {
        _gameFinished = true;
        StartCoroutine(Disconnect());
    }

    private IEnumerator Disconnect()
    {
        yield return new WaitForSeconds(7f);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
}