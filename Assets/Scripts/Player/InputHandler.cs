using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private bool mobile;
    [SerializeField] private int edgesPercent;


    private Vector3 _previousPosition;
    private Vector3 _targetPosition;
    private Camera _mainCamera;
    private Vector3 _returnValue;

    private event Action _onMouseDownAction;
    private event Action _onMouseUpAction;
    private event Action _onMouseAction;

    private Vector3 NormalizedMousePosition =>
        new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0);

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
        _onMouseDownAction += () => _previousPosition = NormalizedMousePosition;
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<InputHandler>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public Vector3 MoveDirection()
    {
        var delta = _previousPosition - NormalizedMousePosition;
        if (IsPointerOverUIObject() == false)
        {
            _previousPosition = NormalizedMousePosition;
            delta = new Vector3(delta.y, 0, -delta.x);
            return delta;
        }
        else
        {
            delta = Vector3.zero;
            return delta;
        }
    }
    
    private void Update()
    {
        ZoomAxis = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButtonUp(0))
        {
            _onMouseUpAction?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _onMouseDownAction?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            _onMouseAction?.Invoke();
        }
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
    
    public RaycastHit GetHitPoint()
    {
        if (IsPointerOverUIObject())
        {
            return new RaycastHit();
        }

        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
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

    public Vector3 MoveDirectionAroundEdges()
    {
        var returnValue = Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        _previousPosition = NormalizedMousePosition;
        returnValue = new Vector3(-returnValue.y, 0, returnValue.x);
        return returnValue.normalized;
    }

    public bool CheckEdges()
    {
        var mousePosition = Input.mousePosition;
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