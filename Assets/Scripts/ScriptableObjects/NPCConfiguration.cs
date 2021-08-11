
    using UnityEngine;

    [CreateAssetMenu(fileName = "New npc", menuName = "NPC Configuration")]
    public class NPCConfiguration : ScriptableObject
    {
        [SerializeField] private string nameNpcInArmy;
        [SerializeField] private GameObject npcGameObject;
        [SerializeField] private int amountOnStart;

        public GameObject NPCGameObject => npcGameObject;
        public int AmountOnStart => amountOnStart;
        public string NameNpcInArmy => nameNpcInArmy;
    }