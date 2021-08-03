using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Movenment : MonoBehaviour
{
    [SerializeField] private float maxZ = 0;
    [SerializeField] private float minZ = 0;
    [SerializeField] private float maxX = 0;
    [SerializeField] private float minX = 0;
    [SerializeField] private float speed = 30f;
    [SerializeField] private float zoom = 0.25f;
    [SerializeField] private float zoomMax = 10;
    [SerializeField] private float zoomMin = 3;

    private float _currentZoom;
    private Vector3 _offset;
    private InputHandler _inputHandler;
    private Vector3 _moveDir;
    private bool _isGround;
    private CharacterController _characterController;

    private void Start()
    {
        _inputHandler = ServiceLocator.GetService<InputHandler>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Zoom();
        Move();
    }

    private void Move()
    {
        if (_inputHandler.MoveDirection() == Vector2.zero) return;
        var direction = _inputHandler.MoveDirection();
        _moveDir = new Vector3(direction.x, 0, direction.y).normalized;
        _moveDir *= speed;
        _characterController.Move(_moveDir * Time.deltaTime);
        var newPos = transform.position;
        newPos.x =   Mathf.Clamp(newPos.x, minX, maxX);
        newPos.z =   Mathf.Clamp(newPos.z, minZ, maxZ);
        transform.position = newPos;
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

        transform.position += _offset;
    }
}