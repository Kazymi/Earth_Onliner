using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    private PlayerResources _playerResources;
    private Builders _builders;
    private void Awake()
    {
        _builders = new Builders();
        _playerResources = new PlayerResources();

        ServiceLocator.Subscribe<PlayerResources>(_playerResources);
        ServiceLocator.Subscribe<Builders>(_builders);
    }
}
