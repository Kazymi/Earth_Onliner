using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public bool IsUnlockIsland { get; private set; }

    public void UnlockIsland()
    {
        Debug.Log("Island Unlock");
        IsUnlockIsland = true;
    }
}
