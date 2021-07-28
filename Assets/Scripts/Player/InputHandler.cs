using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float _zoomAxis;
    private Vector2 _mouseAxis;
    private Camera _mainCamera;
    private bool _positionSelection;

    public bool PositionSelection
    {
        set => _positionSelection = value;
        get => _positionSelection;
    }
    public float ZoomAxis => _zoomAxis;
    public Vector2 MouseAxis => _mouseAxis;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<InputHandler>(this);
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
        _zoomAxis = Input.GetAxis("Mouse ScrollWheel");
        _mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public PositionBuilding GetBuildPosition()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (raycastHit.transform.GetComponent<Earth>())
                {
                    return new PositionBuilding(raycastHit.point+new Vector3(0,2,0), raycastHit.normal);
                }
            }
        }
        return new PositionBuilding(Vector3.zero, Vector3.zero);
    }
}