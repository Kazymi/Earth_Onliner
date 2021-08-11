using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingContractor))]
public class ArmyGenerator : MonoBehaviour
{
    [SerializeField] private NPCConfiguration npcConfiguration;
    [SerializeField] private float timer;
    [SerializeField] private int amount;
    
    private ArmySystem _armySystem;
    private float _currentTimer;

    private void Start()
    {
        _armySystem = ServiceLocator.GetService<ArmySystem>();
        if (GetComponent<BuildingContractor>().IsMine == false)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (_currentTimer < 0)
        {
            _currentTimer = timer;
            _armySystem.AddNewNpcInArmy(npcConfiguration,amount);
        }
        _currentTimer-=Time.deltaTime;
    }

    public void UpdateState(ArmyUpgrade armyUpgrade)
    {
        _currentTimer = 0;
        npcConfiguration = armyUpgrade.NpcConfiguration;
        timer = armyUpgrade.Timer;
        amount = armyUpgrade.Amount;
    }
}
