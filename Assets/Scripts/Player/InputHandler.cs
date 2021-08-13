using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private bool mobile;
    [SerializeField] private int edgesPercent;


    private bool _isBuild;
    private Camera _mainCamera;
    private Vector3 _currentPosition;
    private Vector3 _lastPosition;
    private Vector3 _moveAxis;
    private Vector3 _returnValue;

    private event Action _onMouseDownAction;
    private event Action _onMouseUpAction;
    private event Action _onMouseAction;

    public event Action OnMouseDownAction
    {
        add => _onMouseDownAction += value;
        remove => _onMouseDownAction -= value;
    }

    public event Action OnMouseAction
    {
        add => _onMouseAction += value;
        remove => _onMouseAction -= value;
    }

    public event Action OnMouseUpAction
    {
        add => _onMouseUpAction += value;
        remove => _onMouseUpAction -= value;
    }

    public bool PositionSelection { set; get; }
    public float ZoomAxis { get; private set; }
    public Vector3 MoveVector => _returnValue;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<InputHandler>(this);
        _onMouseUpAction += () => _returnValue = Vector3.zero;
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<InputHandler>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        ZoomAxis = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButtonUp(0)) _onMouseUpAction?.Invoke();
        if (Input.GetMouseButtonDown(0)) _onMouseDownAction?.Invoke();
    }

    public RaycastHit GetHitPoint(LayerMask layerMask)
    {
        if (IsPointerOverUIObject())
        {
            return new RaycastHit();
        }
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
        {
            return raycastHit;
        }
        else return new RaycastHit();
    }

    public PositionBuilding GetStartBuildPosition()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return new PositionBuilding(raycastHit.point, raycastHit.normal);
        }

        return new PositionBuilding(Vector3.zero, Vector3.zero);
    }

    public Vector2 MoveDirectionBuild()
    {
        if (IsPointerOverUIObject())
        {
            return new Vector2();
        }

        if (mobile)
        {
            if (Input.GetMouseButton(0))
            {
                _currentPosition = Input.mousePosition;
                var newPos = _currentPosition - _lastPosition;
                newPos = newPos.normalized;
                _returnValue = Vector3.zero;
                if (_currentPosition != _lastPosition)
                {
                    _returnValue = new Vector3(newPos.y, -newPos.x, 0);
                }

                _lastPosition = _currentPosition;
            }
        }
        else
        {
            _returnValue = new Vector2(Input.GetAxisRaw("Vertical"), -Input.GetAxisRaw("Horizontal"));
        }

        return _returnValue;
    }

    public Vector2 MoveDirection()
    {
        if (IsPointerOverUIObject())
        {
            return new Vector2();
        }

        if (mobile)
        {
            if (Input.GetMouseButton(0))
            {
                _currentPosition = Input.mousePosition;
                var newPos = _currentPosition - _lastPosition;
                newPos = newPos.normalized;
                if (CheckEdges(_currentPosition))
                {
                    _returnValue = Vector3.zero;
                }
                if (_currentPosition != _lastPosition)
                {
                    _returnValue = new Vector3(-newPos.y, newPos.x, 0);
                }
                _lastPosition = _currentPosition;
            }
        }
        else
        {
            _returnValue = new Vector2(Input.GetAxisRaw("Vertical"), -Input.GetAxisRaw("Horizontal"));
        }

        return _returnValue;
    }

    private bool CheckEdges(Vector3 mousePosition)
    {
        var wight = Screen.width;
        var height = Screen.height;
        var percentWightMin = (wight / 100) * edgesPercent;
        var percentHeightMin = (height / 100) * edgesPercent;
        var percentWightMax = wight - percentWightMin;
        var percentHeightMax = height - percentHeightMin;


        if (percentHeightMax > mousePosition.y
            && percentHeightMin < mousePosition.y
            && percentWightMax > mousePosition.x &&
            percentWightMin < mousePosition.x)
        {
            return true;
        }

        return false;
    }

    private bool IsPointerOverUIObject()
    {
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}