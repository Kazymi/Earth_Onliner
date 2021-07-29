using System;
using Photon.Pun;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private InputHandler _inputHandler;
    private Building _newBuild;
    private GameMenu _gameMenu;
    private Earth _earth;
    private PhotonView _photonView;
    private string _currentBuildName;
    private BuildingManager _buildingManager;
    private bool _positionSelected;

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
        _buildingManager = ServiceLocator.GetService<BuildingManager>();
        _inputHandler = ServiceLocator.GetService<InputHandler>();
        _gameMenu = ServiceLocator.GetService<GameMenu>();
        _earth = ServiceLocator.GetService<Earth>();
    }

    public void NewBuild(BuildingConfiguration buildingConfiguration)
    {
        _inputHandler.PositionSelection = true;
        _positionSelected = false;
        if (_newBuild != null)
        {
            _buildingManager.GetFactoryByName(_currentBuildName).Destroy(_newBuild.gameObject);
        }

        _currentBuildName = buildingConfiguration.BuildingGameObject.name;
        _newBuild = _buildingManager.GetBuildingByName(_currentBuildName).GetComponent<Building>();
    }
    
    public void SetPositionBuild()
    {
        if (_newBuild.IsUnlockBuild == false)
        {
            return;
        }

        _newBuild.GetComponent<Building>().Initialize();
        _newBuild.transform.parent = _earth.transform;
        var newBuildTransform = _newBuild.transform;
        _earth.GetComponent<PhotonView>().RPC(RPCEvents.BuildNewBuilding.ToString(), RpcTarget.All, _currentBuildName,
            newBuildTransform.localPosition,
            newBuildTransform.localRotation);
        _inputHandler.PositionSelection = false;
        _newBuild = null;
    }

    private void Update()
    {
        if (_newBuild != null)
        {
            if (_newBuild && _positionSelected)
            {
                _gameMenu.BuildButtonSetState(_newBuild.IsUnlockBuild);
            }

            SetPositionNewBuilding();
        }
        else
        {
            _gameMenu.BuildButtonSetState(false);
        }
    }

    private void SetPositionNewBuilding()
    {
        var pos = _inputHandler.GetBuildPosition();
        if (pos.Position == Vector3.zero)
        {
            return;
        }

        _positionSelected = true;
        _newBuild.transform.position = pos.Position;
        _newBuild.transform.rotation = Quaternion.LookRotation(pos.Normal);
    }
}