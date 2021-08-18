using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingContractor))]
public class ArmyUpgrader : MonoBehaviour,Upgrader
{
    [SerializeField] private PriceConfiguration priceConfiguration;
    [SerializeField] private List<ArmyUpgrade> armyUpgrades;
    [SerializeField] private ArmyGenerator armyGenerator;
    
    private UpgradeSystemMenu _upgradeSystemMenu;

    public int CurrentLvl { get; set; }
    
    private void Start()
    {
        if (GetComponent<BuildingContractor>().IsMine == false)
        {
            Destroy(this);
        }
        _upgradeSystemMenu = ServiceLocator.GetService<UpgradeSystemMenu>();
    }
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
        _upgradeSystemMenu.CloseCanvas();
        CurrentLvl++;
    }
}
