using System.Collections.Generic;
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

    private void Start()
    {
        _inputHandler = ServiceLocator.GetService<InputHandler>();
        _gameMenu = ServiceLocator.GetService<GameMenu>();
        _earth = ServiceLocator.GetService<Earth>();
    }

    public void NewBild(Building newBuildingGameObject)
    {
        _inputHandler.PositionSelection = true;
        if (_newBuild != null)
        {
            Destroy(_newBuild.gameObject);
        }

        _currentBuildName = newBuildingGameObject.name;
        _newBuild = Instantiate(newBuildingGameObject.gameObject).GetComponent<Building>();
    }

    public void SetPositionBild()
    {
        if (_newBuild.UnlockBild == false)
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
            if (_newBuild)
            {
                _gameMenu.BuildButtonSetState(_newBuild.UnlockBild);
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

        _newBuild.transform.position = pos.Position;
        _newBuild.transform.rotation = Quaternion.LookRotation(pos.Normal);
    }
}