public class UpgradeSystem
{
    private UpgradePrice _upgrade;
    private Upgrader _upgrader;
    private PlayerResources _playerResources;

    public UpgradeSystem(UpgradePrice upgrade, Upgrader upgrader)
    {
        _upgrade = upgrade;
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
            _playerResources.RemoveResource(TypeResource.Gold, _upgrade.NeedGold);
            _playerResources.RemoveResource(TypeResource.Iron, _upgrade.NeedIron);
            _playerResources.RemoveResource(TypeResource.Wood, _upgrade.NeedWood);
            _upgrader.UpgradeCompleted();
        }
    }
}