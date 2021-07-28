using UnityEngine;

public class InputHandler : MonoBehaviour
{
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
        if (Input.GetMouseButton(0))
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (raycastHit.transform.GetComponent<Earth>())
                {
                    return new PositionBuilding(raycastHit.point + new Vector3(0, 2, 0), raycastHit.normal);
                }
            }
        }
        return new PositionBuilding(Vector3.zero, Vector3.zero);
    }
}