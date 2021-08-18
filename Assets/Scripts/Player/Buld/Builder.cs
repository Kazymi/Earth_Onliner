using Photon.Pun;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private InputHandler _inputHandler;
    private NewBuilding _newBuild;
    private BuildSystemMenu _buildSystemMenu;
    private Earth _earth;
    private string _currentBuildName;
    private BuildingConfiguration _currentBuildingConfiguration;
    private BuyConfiguration _buyConfiguration;
    private BuildingManager _buildingManager;
    private PlayerResources _playerResources;
    private PlayerSystem _playerSystem;
    private BuildSystem _buildSystem;
    private BuiltHouses _builtHouses;

    private void Start()
    {
        _buildSystemMenu = ServiceLocator.GetService<BuildSystemMenu>();
        _playerResources = ServiceLocator.GetService<PlayerResources>();
        _buildingManager = ServiceLocator.GetService<BuildingManager>();
        _inputHandler = ServiceLocator.GetService<InputHandler>();
        _playerSystem = ServiceLocator.GetService<PlayerSystem>();
        _buildSystem = ServiceLocator.GetService<BuildSystem>();
        _earth = ServiceLocator.GetService<Earth>();
        _builtHouses = ServiceLocator.GetService<BuiltHouses>();
    }

    private void Update()
    {
        if (_newBuild != null)
        {
            _buildSystemMenu.UnlockBuildButton(_newBuild.CheckPosition());
        }
        else
        {
            _buildSystemMenu.UnlockBuildButton(false);
        }
    }
    
    public void StopBuild()
    {
        _buildingManager.GetFactoryByName(_currentBuildName).Destroy(_newBuild.gameObject);
        _newBuild = null;
        _playerSystem.BuildComplete();
        _buildSystemMenu.ActivateCanvas(false);
    }

    public void NewBuild(BuildingConfiguration buildingConfiguration, BuyConfiguration buyConfiguration)
    {
        _builtHouses.NewBuild(buyConfiguration.BuildingConfiguration);
        _buyConfiguration = buyConfiguration;
        _inputHandler.PositionSelection = true;
        if (_newBuild != null)
        {
            _buildingManager.GetFactoryByName(_currentBuildName).Destroy(_newBuild.gameObject);
        }

        _currentBuildingConfiguration = buildingConfiguration;
        _currentBuildName = buildingConfiguration.BuildingGameObject.name;
        _newBuild = _buildingManager.GetBuildingByName(_currentBuildName).GetComponent<NewBuilding>();
        _newBuild.transform.position = _inputHandler.GetStartBuildPosition().Position;
        _playerSystem.StartBuild(_newBuild);
        _buildSystemMenu.ActivateCanvas(true);
        _buildSystem.Initialize(_newBuild.transform, this);
    }

    public void SetPositionBuild()
    {
        if (_newBuild.CheckPosition() == false)
        {
            return;
        }

        _newBuild.transform.parent = _earth.transform;
        var newBuildTransform = _newBuild.transform;
        _earth.GetComponent<PhotonView>().RPC(RPCEvents.BuildNewBuilding.ToString(), RpcTarget.All, _currentBuildName,
            newBuildTransform.localPosition,
            newBuildTransform.localRotation,
            PhotonNetwork.LocalPlayer.UserId);
        _inputHandler.PositionSelection = false;
        _buildingManager.GetFactoryByName(_currentBuildName).Destroy(_newBuild.gameObject);
        _newBuild = null;
        _playerResources.RemoveResource(TypeResource.Gold, _buyConfiguration.NeedGold);
        _playerResources.RemoveResource(TypeResource.Iron, _buyConfiguration.NeedIron);
        _playerResources.RemoveResource(TypeResource.Wood, _buyConfiguration.NeedWood);
        _playerSystem.BuildComplete();
        _buildSystemMenu.ActivateCanvas(false);
        _builtHouses.AddBuilding(_currentBuildingConfiguration);
    }
}