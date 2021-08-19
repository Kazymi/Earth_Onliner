using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNameSystem
{

    private string _currentNickName;
    private const string _saveName = "Nickname";

    public string CurrentNickName => _currentNickName;

    public PlayerNameSystem()
    {
        var nick = PlayerPrefs.GetString(_saveName);
        if (string.IsNullOrEmpty(nick))
        {
            nick = "Player" + Random.Range(0, 1000);
        }
        UpdateNickName(nick);
    }

    public void UpdateNickName(string newText)
    {
        _currentNickName = newText;
        PhotonNetwork.NickName = newText;
        PlayerPrefs.SetString(_saveName, newText);
    }
}