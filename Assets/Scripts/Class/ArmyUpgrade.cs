
    using System;
    using UnityEngine;

    [Serializable]
    public class ArmyUpgrade
    {
        [SerializeField] private NPCConfiguration npcConfiguration;
        [SerializeField] private float timer;
        [SerializeField] private int amount;

        public NPCConfiguration NpcConfiguration => npcConfiguration;
        public float Timer => timer;
        public int Amount => amount;
    }