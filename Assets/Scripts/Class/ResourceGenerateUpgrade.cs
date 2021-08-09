
    using System;
    using UnityEngine;

    [Serializable]
    public class ResourceGenerateUpgrade
    {
        [SerializeField] private ResourceGenerate newGold;
        [SerializeField] private ResourceGenerate newWood;
        [SerializeField] private ResourceGenerate newIron;
        
        public ResourceGenerate NewGold => newGold;

        public ResourceGenerate NewWood => newWood;

        public ResourceGenerate NewIron => newIron;
    }
