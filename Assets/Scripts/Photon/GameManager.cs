using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PhotonView))]
public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private bool _gameFinished;
    private PhotonView _photonView;
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

    public void MainHouseDestroy()
    {
        object[] content = new object[]{_photonView.Controller.NickName};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
        PhotonNetwork.RaiseEvent((int) EventType.FinishGame, content, raiseEventOptions,
            SendOptions.SendReliable);
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