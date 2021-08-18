using System.Collections.Generic;
using UnityEngine;

public class ArmySystem : MonoBehaviour
{
    [SerializeField] private List<NPCConfiguration> npcConfigurations;
    private Dictionary<NPCConfiguration, ArmyPanel> _armyPanels;
    private Dictionary<NPCConfiguration, int> _amountArmy;
    public Dictionary<NPCConfiguration, int> AmountArmy => _amountArmy;

    public void Initialize(Transform armyTransform, ArmyPanel armyPanel)
    {
        _armyPanels = new Dictionary<NPCConfiguration, ArmyPanel>();
        _amountArmy = new Dictionary<NPCConfiguration, int>();
        foreach (var npc in npcConfigurations)
        {
            _amountArmy.Add(npc,0);
            var newGameObject = Instantiate(armyPanel, armyTransform);
            _armyPanels.Add(npc, newGameObject);
            newGameObject.Initialize(npc.NameNpcInArmy);
        }
    }

    public void AddNewNpcInArmy(NPCConfiguration npcConfiguration, int amount)
    {
        _amountArmy[npcConfiguration] += amount;
        UpdateState(npcConfiguration);
    }

    public void RemoveNpcInArmy(NPCConfiguration npcConfiguration, int amount)
    {
        _amountArmy[npcConfiguration] -= amount;
        UpdateState(npcConfiguration);
    }
    
    public bool Compare(NPCConfiguration npcConfiguration,int amount)
    {
        return _amountArmy[npcConfiguration] >= amount;
    }
    
    private void UpdateState(NPCConfiguration npcConfiguration)
    {
        _armyPanels[npcConfiguration].UpdateState(_amountArmy[npcConfiguration]);
    }
}