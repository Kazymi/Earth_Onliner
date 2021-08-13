using Photon.Pun;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private IMovement _movement;
    private IDamageable _health;
    private bool _isMine;

    public bool IsMine => _isMine;
    public IMovement Movement => _movement;
    public IDamageable Health => _health;
    
    [PunRPC]
    public void SpawnNPC(string name)
    {
        _isMine = PhotonNetwork.LocalPlayer.UserId == name;
        InitializeComponent();

        _movement?.Initialize(_isMine);
        if (_health != null)
        {
            _health.IsMine = _isMine;
            _health.Initialize();
        }
    }

    private void InitializeComponent()
    {
        _movement ??= GetComponent<IMovement>() ?? GetComponentInChildren<IMovement>();
        _health ??= GetComponent<IDamageable>() ?? GetComponentInChildren<IDamageable>();
    }
}