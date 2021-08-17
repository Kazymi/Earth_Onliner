using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    private PlayerResources _playerResources;
    private Builders _builders;
    private CameraMovementChecker _cameraMovementChecker;
    private SpawnArmySystem _spawnArmySystem;
    
    
    private void Awake()
    {
        _builders = new Builders();
        _playerResources = new PlayerResources();
        _spawnArmySystem = new SpawnArmySystem();
        _cameraMovementChecker = new CameraMovementChecker();
        
        ServiceLocator.Subscribe<CameraMovementChecker>(_cameraMovementChecker);
        ServiceLocator.Subscribe<SpawnArmySystem>(_spawnArmySystem);
        ServiceLocator.Subscribe<PlayerResources>(_playerResources);
        ServiceLocator.Subscribe<Builders>(_builders);
    }
}
