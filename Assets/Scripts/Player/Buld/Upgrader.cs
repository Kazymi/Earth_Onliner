using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField] private GeneratorResource _generatorResource;
    [SerializeField] private List<Upgrade> _upgrades;
    
    private UpgradeSystemMenu _upgradeSystemMenu;
    private int _currentLvl;

    public void Start()
    {
        _upgradeSystemMenu = ServiceLocator.GetService<UpgradeSystemMenu>();
    }
    

    public void MouseDrag()
    {
        if (_currentLvl < _upgrades.Count)
            _upgradeSystemMenu.NewUpgrade(_upgrades[_currentLvl], _generatorResource, this);
    }

    public void UpgradeCompleted()
    {
        _currentLvl++;
    }
}