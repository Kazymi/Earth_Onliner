using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementChecker
{
    private InputHandler _inputHandler;
    private bool _isBuild;

    public bool IsBuild
    {
        set => _isBuild = value;
    }

    public CameraMovementChecker()
    {
        _inputHandler = ServiceLocator.GetService<InputHandler>();
    }
    public Vector3 GetMoveDirection()
    {
        if (_isBuild)
        {
            return Vector3.zero;
        }
        else
        {
            if (_inputHandler.CheckEdges() == false)
            {
                return _inputHandler.MoveDirectionAroundEdges() * Time.deltaTime;
            }
            else
            {
                return _inputHandler.MoveDirection();
            }
        }
    }

}
