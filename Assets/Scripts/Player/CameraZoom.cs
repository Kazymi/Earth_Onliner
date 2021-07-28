using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoom = 0.25f;
    [SerializeField] private float zoomMax = 10;
    [SerializeField] private float zoomMin = 3;

    private Vector3 _offset;
    private float _currentZoom;
    private InputHandler _inputHandler;

    private void Start()
    {
        _inputHandler = ServiceLocator.GetService<InputHandler>();
    }

    void Update()
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

        transform.position += _offset;
    }
}