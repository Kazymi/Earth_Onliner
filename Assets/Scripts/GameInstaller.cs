using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    private PlayerResources _playerResources;
    private Builders _builders;
    private CameraMovementChecker _cameraMovementChecker;
    private SpawnArmySystem _spawnArmySystem;
    private BuildSystem _buildSystem;
    private BuiltHouses _builtHouses;
    
    
    private void Awake()
    {
        _buildSystem = new BuildSystem();
        _builders = new Builders();
        _playerResources = new PlayerResources();
        _spawnArmySystem = new SpawnArmySystem();
        _cameraMovementChecker = new CameraMovementChecker();
        _builtHouses = new BuiltHouses();
        
        ServiceLocator.Subscribe<BuiltHouses>(_builtHouses);
        ServiceLocator.Subscribe<BuildSystem>(_buildSystem);
        ServiceLocator.Subscribe<CameraMovementChecker>(_cameraMovementChecker);
        ServiceLocator.Subscribe<SpawnArmySystem>(_spawnArmySystem);
        ServiceLocator.Subscribe<PlayerResources>(_playerResources);
        ServiceLocator.Subscribe<Builders>(_builders);
    }
}
