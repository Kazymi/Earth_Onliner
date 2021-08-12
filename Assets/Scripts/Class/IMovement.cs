

using UnityEngine;

public interface IMovement
{
   void SetNewTarget(Transform target);
   void Initialize(bool isMine);
}
