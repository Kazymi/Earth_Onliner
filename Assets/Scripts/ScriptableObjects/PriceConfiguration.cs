
    using System.Collections.Generic;
    using UnityEngine;
    [CreateAssetMenu(fileName = "New price", menuName = "price Configuration")]
    public class PriceConfiguration : ScriptableObject
    {
        [SerializeField] private List<UpgradePrice> prices;
        [SerializeField] private string description;

        public List<UpgradePrice> Prices => prices;
        public string Description => description;
    }
