using EventBusSystem;
using UnityEngine;

public class BuildState : State
{
    private LayerMask _idLayerBuild;
    private LayerMask _idLayerEarth;
    private InputHandler _inputHandler;

    private BuildingContractor _buildingContractor;
    private BuildingContractor _currentBuildingConstractor;
    private CameraMovementChecker _cameraMovementChecker;

    public BuildState(InputHandler inputHandler, LayerMask layerMask, LayerMask earthLayerMask,CameraMovementChecker cameraMovementChecker)
    {
        _cameraMovementChecker = cameraMovementChecker;
        _inputHandler = inputHandler;
        _idLayerBuild = layerMask;
        _idLayerEarth = earthLayerMask;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _inputHandler.OnMouseUpAction += OnMouseUp;
        _inputHandler.OnMouseDownAction += OnMouseDown;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _inputHandler.OnMouseUpAction -= OnMouseUp;
        _inputHandler.OnMouseDownAction -= OnMouseDown;
    }

    public void Initialize(BuildingContractor buildGameObject)
    {
        _buildingContractor = buildGameObject;
    }

    public override void Tick()
    {
        if (_currentBuildingConstractor != null)
        {
            SetPosition();
        }
    }

    private void OnMouseUp()
    {
        _currentBuildingConstractor = null;
        _cameraMovementChecker.IsBuild = false;
    }
    private void OnMouseDown()
    {
        var hit = _inputHandler.GetHitPoint(_idLayerBuild);
        if (hit.collider == null)
        {
            return;
        }

        var buildingContractor = hit.collider.GetComponent<BuildingContractor>();
        if (buildingContractor == false)
        {
            return;
        }

        if (_buildingContractor == buildingContractor)
        {
            _currentBuildingConstractor = buildingContractor;
            _cameraMovementChecker.IsBuild = true;
        }
    }

    private void SetPosition()
    {
        var hit = _inputHandler.GetHitPoint(_idLayerEarth);
        var pos = hit.point;
        if (pos == Vector3.zero)
        {
            return;
        }
        _currentBuildingConstractor.transform.position = pos;
    }
}