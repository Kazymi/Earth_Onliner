using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private float radiusBuilding;


    private bool _isConstructionAllowed;

    private bool _isBuild;

    public bool IsUnlockBuild => CheckPosition();

    public void Initialize()
    {
        _isBuild = true;
    }

    private bool CheckPosition()
    {
        // TODO: layers can be used to avoid GetComponent
        var allFindGameObject = Physics.OverlapBox(transform.position, new Vector3(radiusBuilding, radiusBuilding, radiusBuilding));
        foreach (var findGameObject in allFindGameObject)
        {
            var building = findGameObject.GetComponent<Building>();
            if (building)
            {
                if (building != this)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - new Vector3(0, 2, 0), new Vector3(radiusBuilding, 1, radiusBuilding));
    }
}