
    public class BuySystem
    {
        private BuyConfiguration _buyConfiguration;
        private Builder _builder;
        private PlayerResources _playerResources;
        private ShopSystemMenu _systemShopMenu;
        
        public BuySystem(BuyConfiguration buyConfiguration,ShopSystemMenu systemMenu)
        {
            _buyConfiguration = buyConfiguration;
            _builder = ServiceLocator.GetService<Builder>();
            _playerResources = ServiceLocator.GetService<PlayerResources>();
            _systemShopMenu = systemMenu;
        }
        
        public void Buy()
        {
            if (IsUnlockedBuy())
            {
                _builder.NewBuild(_buyConfiguration.BuildingConfiguration);
                _systemShopMenu.CloseShop();
            }
        }

        public bool IsUnlockedBuy()
        {
            if (_playerResources.CheckAvailability(TypeResource.Gold, _buyConfiguration.NeedGold) == false ||
                _playerResources.CheckAvailability(TypeResource.Iron, _buyConfiguration.NeedIron) == false ||
                _playerResources.CheckAvailability(TypeResource.Wood, _buyConfiguration.NeedWood) == false)
                return false;
            return true;
        }
    }