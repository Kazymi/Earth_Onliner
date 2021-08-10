using System;
using EventBusSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class Movenment : MonoBehaviour, IBuildEvent
{
    [SerializeField] private float maxZ;
    [SerializeField] private float minZ;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    [SerializeField] private float speed = 30f;
    [SerializeField] private float speedBuild = 60f;
    [SerializeField] private float zoom = 0.25f;
    [SerializeField] private float zoomMax = 10;
    [SerializeField] private float zoomMin = 3;

    private float _currentZoom;
    private float _currentSpeed;
    private bool _isBuild;
    private Vector3 _offset;
    private InputHandler _inputHandler;
    private Vector3 _moveDir;
    private bool _isGround;
    private CharacterController _characterController;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void Start()
    {
        _inputHandler = ServiceLocator.GetService<InputHandler>();
        _characterController = GetComponent<CharacterController>();
        _currentSpeed = speed;
    }

    private void Update()
    {
        Zoom();
        Move();
    }

    private void Move()
    {
        var direction = _inputHandler.MoveDirection();
        if (direction == Vector2.zero) return;
        _moveDir = new Vector3(direction.x, 0, direction.y).normalized;
        // TODO: use state machine
        if (_isBuild)
        {
            _currentSpeed = speedBuild;
        }
        else
        {
            _currentSpeed = speed + _currentZoom;
        }

        // TODO: controls are inverted
        _moveDir *= _currentSpeed;
        _characterController.Move(_moveDir * Time.deltaTime);
        var newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);
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

    public void OnBuild()
    {
        _isBuild = true;
    }

    public void OnUpgrade()
    {
        _isBuild = false;
    }
}