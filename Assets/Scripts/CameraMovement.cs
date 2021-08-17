using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float maxZ;
    [SerializeField] private float minZ;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;

    [SerializeField] private Transform mainCamera;
    [SerializeField] private float sensitivity = 500;
    [SerializeField] private float interpolationSpeed = 5;
    
    [SerializeField] private float zoom = 0.25f;
    [SerializeField] private float zoomMax = 10;
    [SerializeField] private float zoomMin = 3;

    private InputHandler _inputHandler;
    private CameraMovementChecker _cameraMovementChecker;
    private Vector3 _offset;
    private float _currentZoom;
    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPosition = mainCamera.position;
        _cameraMovementChecker = ServiceLocator.GetService<CameraMovementChecker>();
        _inputHandler = ServiceLocator.GetService<InputHandler>();
        _inputHandler.OnMouseAction += Move;
    }

    private void OnEnable()
    {
        if (_inputHandler != null)
        {
            _inputHandler.OnMouseAction += Move;
        }
    }

    private void OnDisable()
    {
        _inputHandler.OnMouseAction -= Move;
    }

    private void Update()
    {
        var newPos = Vector3.Lerp(mainCamera.position, _targetPosition, Time.deltaTime * interpolationSpeed);
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);
        mainCamera.position = newPos;
        Zoom();
    }

    private void Move()
    {
        var delta = _cameraMovementChecker.GetMoveDirection();
        _targetPosition += delta * sensitivity * Time.deltaTime;
    }
    
    private void Zoom()
    {
        _offset = new Vector3();
        if (_inputHandler.ZoomAxis == 0) return;
        if (_inputHandler.ZoomAxis > 0 && _currentZoom < zoomMax)
        {
            _currentZoom += zoom;
            _offset.y += zoom;
        }

        if (_inputHandler.ZoomAxis < 0 && _currentZoom > zoomMin)
        {
            _currentZoom -= zoom;
            _offset.y -= zoom;
        }

        _targetPosition += _offset;
    }
}