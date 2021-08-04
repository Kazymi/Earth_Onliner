using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Upgrader : MonoBehaviour
{
    [SerializeField] private List<Upgrade> upgrades = new List<Upgrade>();
    [SerializeField] private GeneratorResource generatorResource;

    private bool _initialized;
    private int _currentLvl;
    private UpgradeSystemMenu _upgradeSystemMenu;

    private void OnMouseDrag()
    {
        // TODO: avoid continues condition checks
        if (_initialized)
            _upgradeSystemMenu.NewUpgrade(upgrades[_currentLvl], generatorResource, this);
    }

    private void Start()
    {
        _upgradeSystemMenu = ServiceLocator.GetService<UpgradeSystemMenu>();
    }

    public void UpgradeCompleted()
    {
        _currentLvl++;
    }

    public void Initialize()
    {
        _initialized = true;
    }
}