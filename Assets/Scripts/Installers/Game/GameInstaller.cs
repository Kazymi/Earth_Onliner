using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private Earth earth;
    [SerializeField]private Builder builder;
    
    private PlayerResources _playerResources;
    private Builders _builders;
    private CameraMovementChecker _cameraMovementChecker;
    private BuiltHouses _builtHouses;
    
    
    private void Awake()
    {
        _builders = new Builders();
        _playerResources = new PlayerResources();
        _cameraMovementChecker = new CameraMovementChecker();
        _builtHouses = new BuiltHouses();
        
        ServiceLocator.Subscribe<Builder>(builder);
        ServiceLocator.Subscribe<Earth>(earth);
        ServiceLocator.Subscribe<BuiltHouses>(_builtHouses);
        ServiceLocator.Subscribe<CameraMovementChecker>(_cameraMovementChecker);
        ServiceLocator.Subscribe<PlayerResources>(_playerResources);
        ServiceLocator.Subscribe<Builders>(_builders);
    }
}
