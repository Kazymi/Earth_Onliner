using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private bool mobile;
    [SerializeField] private Joystick playerJoystick;

    private Camera _mainCamera;
    public bool PositionSelection { set; get; }
    public float ZoomAxis { get; private set; }
    public Vector2 MouseAxis { get; private set; }

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
        ZoomAxis = Input.GetAxis("Mouse ScrollWheel");
        MouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public PositionBuilding GetBuildPosition()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.GetComponent<Earth>())
            {
                return new PositionBuilding(raycastHit.point + new Vector3(0, 2, 0), raycastHit.normal);
            }
        }

        return new PositionBuilding(Vector3.zero, Vector3.zero);
    }
    
    public PositionBuilding GetStartBuildPosition()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return new PositionBuilding(raycastHit.point + new Vector3(0, 2, 0), raycastHit.normal);
        }
        return new PositionBuilding(Vector3.zero, Vector3.zero);
    }

    public Vector2 MoveDirection()
    {
        if (mobile)
        {
            return new Vector2(playerJoystick.Direction.y, -playerJoystick.Direction.x);
        }
        else
        {
            return new Vector2(Input.GetAxisRaw("Vertical"), -Input.GetAxisRaw("Horizontal"));
        }
    }
}