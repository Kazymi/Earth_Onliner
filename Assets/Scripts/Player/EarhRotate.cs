using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EarhRotate : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float rotateSpeed = 30f;
    [SerializeField] private float zoom = 0.25f;
    [SerializeField] private float zoomMax = 10;
    [SerializeField] private float zoomMin = 3;
    
    private float _currentZoom;
    private Camera _camera;
    private Vector3 _offset;
    private InputHandler _inputHandler;

    private void Start()
    {
        _camera = Camera.main;
        _inputHandler = ServiceLocator.GetService<InputHandler>();
    }

    private void Update()
    {
        Zoom();
    }

    void OnMouseDrag()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (_inputHandler.PositionSelection)
        {
            return;
        }
        var mouseAxis = _inputHandler.MouseAxis * rotateSpeed;
        var position = transform.position;
        var transformCamera = _camera.transform;
        var positionCamera = transformCamera.position;
        var right = Vector3.Cross(transformCamera.up, position - positionCamera);
        var up = Vector3.Cross(position - positionCamera, right);
        var rotation = transform.rotation;
        rotation = Quaternion.AngleAxis(-mouseAxis.x, up) * rotation;
        rotation = Quaternion.AngleAxis(mouseAxis.y, right) * rotation;
        transform.rotation = rotation;
    }
    
    private void Zoom()
    {
        _offset = new Vector3();
        if (_inputHandler.ZoomAxis == 0) return;
        if (_inputHandler.ZoomAxis > 0 && _currentZoom < zoomMax)
        {
            _currentZoom += zoom;
            _offset.z += zoom;
        }

        if (_inputHandler.ZoomAxis < 0 && _currentZoom > zoomMin)
        {
            _currentZoom -= zoom;
            _offset.z -= zoom;
        }

        cameraPosition.transform.position += _offset;
    }
}