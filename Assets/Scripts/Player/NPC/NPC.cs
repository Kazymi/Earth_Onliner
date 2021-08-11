using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private INPC movement;
    private IDamageable health;

    [PunRPC]
    public void SpawnNPC(string name)
    {
        var isMine = PhotonNetwork.LocalPlayer.UserId == name;
        InitializeComponent();

        movement?.Initialize(isMine);
        if (health != null)
        {
            health.IsMine = isMine;
            health.Initialize();
        }
    }

    private void InitializeComponent()
    {
        movement ??= GetComponent<INPC>() ?? GetComponentInChildren<INPC>();
        health ??= GetComponent<IDamageable>() ?? GetComponentInChildren<IDamageable>();
    }
}