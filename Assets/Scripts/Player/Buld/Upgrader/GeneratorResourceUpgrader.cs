using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GeneratorResourceUpgrader : MonoBehaviour, Upgrader
{
    [SerializeField] private GeneratorResource generatorResource;
    [SerializeField] private List<ResourceGenerateUpgrade> resourceGenerateUpgrade;
    [SerializeField] private PriceConfiguration priceConfiguration;

    private UpgradeSystemMenu _upgradeSystemMenu;
    public int CurrentLvl { get; set; }

    private void Start()
    {
        if (GetComponent<BuildingContractor>().IsMine == false)
        {
            Destroy(this);
        }
        _upgradeSystemMenu = ServiceLocator.GetService<UpgradeSystemMenu>();
        CurrentLvl = 0;
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
        if (resourceGenerateUpgrade[CurrentLvl].NewGold != new ResourceGenerate())
            generatorResource.AddResource(resourceGenerateUpgrade[CurrentLvl].NewGold);
        if (resourceGenerateUpgrade[CurrentLvl].NewIron != new ResourceGenerate())
            generatorResource.AddResource(resourceGenerateUpgrade[CurrentLvl].NewIron);
        if (resourceGenerateUpgrade[CurrentLvl].NewWood != new ResourceGenerate())
            generatorResource.AddResource(resourceGenerateUpgrade[CurrentLvl].NewWood);
        CurrentLvl++;
        _upgradeSystemMenu.CloseCanvas();
    }
}