using EventBusSystem;

    public interface IBuildEvent: IGlobalSubscriber
    {
        void OnBuild();
        void OnUpgrade();
    }