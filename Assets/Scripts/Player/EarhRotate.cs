using UnityEngine;

public class EarhRotate : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 30f;

    private Camera _camera;
    private Vector3 _offset;
    private InputHandler _inputHandler;

    private void Start()
    {
        _camera = Camera.main;
        _inputHandler = ServiceLocator.GetService<InputHandler>();
    }

    void OnMouseDrag()
    {
        if (_inputHandler.PositionSelection)
        {
            return;
        }

        // TODO: add inertia to rotation and zoom
        var mouseAxis = _inputHandler.MouseAxis * rotSpeed;
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
}