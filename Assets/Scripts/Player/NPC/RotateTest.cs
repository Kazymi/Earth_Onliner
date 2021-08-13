using UnityEngine;

public class RotateTest : MonoBehaviour
{
    [SerializeField] private Transform target;
    void Update()
    {
        transform.RotateAround(target.position,Vector3.down, 20*Time.deltaTime);
    }
}
