
    public  interface IFactoryInitialize
    {
        Factory ParentFactory { get; set; }
        void Initialize();
    }
