using System.Collections.Generic;
using UnityEngine;

public interface Upgrader
{
    int CurrentLvl { get; set; }
    public void OnMouseDownAction();
    public void UpgradeCompleted();
}