using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    private PlayerResources _playerResources;

    private void Awake()
    {
        _playerResources = new PlayerResources();
        
        ServiceLocator.Subscribe<PlayerResources>(_playerResources);
    }
}
