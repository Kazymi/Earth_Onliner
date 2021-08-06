using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private InputHandler _inputHandler;
    private BuildingContractor _newBuild;
    private GameMenu _gameMenu;
    private Earth _earth;
    private PhotonView _photonView;
    private string _currentBuildName;
    private BuyConfiguration _buyConfiguration;
    private BuildingManager _buildingManager;
    private bool _positionSelected;
    private Dictionary<BuildingConfiguration, int> _housesBuilt = new Dictionary<BuildingConfiguration, int>();
    private PlayerResources _playerResources;
    private PlayerSystem _playerSystem;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<Builder>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<Builder>();
    }

    private void Start()
    {
        _playerResources = ServiceLocator.GetService<PlayerResources>();
        _buildingManager = ServiceLocator.GetService<BuildingManager>();
        _inputHandler = ServiceLocator.GetService<InputHandler>();
        _playerSystem = ServiceLocator.GetService<PlayerSystem>();
        _gameMenu = ServiceLocator.GetService<GameMenu>();
        _earth = ServiceLocator.GetService<Earth>();
    }

    public bool IsUnlockBuilding(BuildingConfiguration buildingConfiguration)
    {
        if (_housesBuilt.ContainsKey(buildingConfiguration) == false)
        {
            return true;
        }

        if (_housesBuilt[buildingConfiguration] >= buildingConfiguration.MaxAmount)
        {
            return false;
        }

        return true;
    }

    public void NewBuild(BuildingConfiguration buildingConfiguration, BuyConfiguration buyConfiguration)
    {
        if (_housesBuilt.ContainsKey(buildingConfiguration) == false)
        {
            _housesBuilt.Add(buildingConfiguration, 0);
        }

        _buyConfiguration = buyConfiguration;
        _housesBuilt[buildingConfiguration]++;
        _inputHandler.PositionSelection = true;
        _positionSelected = false;
        if (_newBuild != null)
        {
            _buildingManager.GetFactoryByName(_currentBuildName).Destroy(_newBuild.gameObject);
        }

        _currentBuildName = buildingConfiguration.BuildingGameObject.name;
        _newBuild = _buildingManager.GetBuildingByName(_currentBuildName).GetComponent<BuildingContractor>();
        _newBuild.transform.position = _inputHandler.GetStartBuildPosition().Position;
        _positionSelected = true;
        _playerSystem.StartBuild(_newBuild);
    }

    public void SetPositionBuild()
    {
        if (CheckPosition() == false)
        {
            return;
        }

        _newBuild.transform.parent = _earth.transform;
        var newBuildTransform = _newBuild.transform;
        _earth.GetComponent<PhotonView>().RPC(RPCEvents.BuildNewBuilding.ToString(), RpcTarget.All, _currentBuildName,
            newBuildTransform.localPosition,
            newBuildTransform.localRotation);
        _inputHandler.PositionSelection = false;
        Destroy(_newBuild.gameObject);
        _newBuild = null;
        _playerResources.RemoveResource(TypeResource.Gold, _buyConfiguration.NeedGold);
        _playerResources.RemoveResource(TypeResource.Iron, _buyConfiguration.NeedIron);
        _playerResources.RemoveResource(TypeResource.Wood, _buyConfiguration.NeedWood);
        _playerSystem.BuildComplete();
    }

    private void Update()
    {
        // TODO: use events or just call method once
        if (_newBuild != null)
        {
            if (_newBuild && _positionSelected)
            {
                _gameMenu.BuildButtonSetState(CheckPosition());
            }
        }
        else
        {
            _gameMenu.BuildButtonSetState(false);
        }
    }

    private bool CheckPosition()
    {
        var returnValue = true;
        var allFindGameObject =
            Physics.OverlapBox(_newBuild.transform.position,
                new Vector3(_newBuild.RadiusBuilding, _newBuild.RadiusBuilding, _newBuild.RadiusBuilding));

        foreach (var findGameObject in allFindGameObject)
        {
            var building = findGameObject.GetComponent<BuildingContractor>();
            var resource = findGameObject.GetComponent<ResourceForConstruction>();

            if (building)
            {
                if (building != _newBuild)
                {
                    returnValue = false;
                    break;
                }
            }

            if (resource)
            {
                if (resource.BuildingResources == BuildingResource.Water)
                {
                    returnValue = false;
                    break;
                }

                if (resource.BuildingResources == _newBuild.Resource)
                {
                    returnValue = true;
                }
            }

            if (_newBuild.Resource == BuildingResource.Nothing)
            {
                returnValue = true;
            }
        }
        
        return returnValue;
    }
}