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

    private bool _positionSelected;

    private void Start()
    {
        _inputHandler = ServiceLocator.GetService<InputHandler>();
        _gameMenu = ServiceLocator.GetService<GameMenu>();
        _earth = ServiceLocator.GetService<Earth>();
    }

    public void NewBuild(Building newBuildingGameObject)
    {
        _inputHandler.PositionSelection = true;
        _positionSelected = false;
        if (_newBuild != null)
        {
            // TODO: add pool
            Destroy(_newBuild.gameObject);
        }

        _currentBuildName = newBuildingGameObject.name;
        // TODO: add factory
        _newBuild = Instantiate(newBuildingGameObject.gameObject).GetComponent<Building>();
    }

    // TODO: subscribe to events in script
    public void SetPositionBuild()
    {
        // TODO: usually bool's name start with is or has
        if (_newBuild.UnlockBuild == false)
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
                _gameMenu.BuildButtonSetState(_newBuild.UnlockBuild);
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