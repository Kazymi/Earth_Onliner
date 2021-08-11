using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingContractor))]
public class ArmyUpgrader : MonoBehaviour,Upgrader
{
    [SerializeField] private PriceConfiguration priceConfiguration;
    [SerializeField] private List<ArmyUpgrade> armyUpgrades;
    [SerializeField] private ArmyGenerator armyGenerator;
    
    private UpgradeSystemMenu _upgradeSystemMenu;

    private void Start()
    {
        if (GetComponent<BuildingContractor>().IsMine == false)
        {
            Destroy(this);
        }
        _upgradeSystemMenu = ServiceLocator.GetService<UpgradeSystemMenu>();
    }

    public int CurrentLvl { get; set; }
    public void OnMouseDownAction()
    {
        if (CurrentLvl <= priceConfiguration.Prices.Count - 1)
        {
            _upgradeSystemMenu.NewUpgrade(priceConfiguration.Prices[CurrentLvl], this, priceConfiguration.Description);
        }
    }

    public void UpgradeCompleted()
    {
        armyGenerator.UpdateState(armyUpgrades[CurrentLvl]);
        CurrentLvl++;
    }
}
