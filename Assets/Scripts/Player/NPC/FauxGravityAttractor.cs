
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour
{
    [SerializeField] private float gravity = -10;

    public void Attract(Transform body,Rigidbody rigidbody)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        
        rigidbody.AddForce(gravityUp*gravity);
        Quaternion target = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation,target,50*Time.deltaTime);
    }
}
