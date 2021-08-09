public class UpgradeSystem
{
    private Upgrade _upgrade;
    private GeneratorResource _generatorResource;
    private Upgrader _upgrader;
    private PlayerResources _playerResources;

    public UpgradeSystem(Upgrade upgrade, GeneratorResource generatorResource, Upgrader upgrader)
    {
        _upgrade = upgrade;
        _generatorResource = generatorResource;
        _upgrader = upgrader;
        _playerResources = ServiceLocator.GetService<PlayerResources>();
    }

    public bool IsUnlockUpgrade()
    {
        return _playerResources.CheckAvailability(TypeResource.Gold, _upgrade.NeedGold) &&
               _playerResources.CheckAvailability(TypeResource.Iron, _upgrade.NeedIron) &&
               _playerResources.CheckAvailability(TypeResource.Wood, _upgrade.NeedWood);
    }

    public void Upgrade()
    {
        if (IsUnlockUpgrade())
        {
            if (_upgrade.NewGold != new ResourceGenerate()) _generatorResource.AddResource(_upgrade.NewGold);
            if (_upgrade.NewIron != new ResourceGenerate()) _generatorResource.AddResource(_upgrade.NewIron);
            if (_upgrade.NewWood != new ResourceGenerate()) _generatorResource.AddResource(_upgrade.NewWood);

            _playerResources.RemoveResource(TypeResource.Gold, _upgrade.NeedGold);
            _playerResources.RemoveResource(TypeResource.Iron, _upgrade.NeedIron);
            _playerResources.RemoveResource(TypeResource.Wood, _upgrade.NeedWood);
            _upgrader.UpgradeCompleted();
        }
    }
}