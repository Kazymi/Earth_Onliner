using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(BuildingContractor))]
public class ArmySpawner : MonoBehaviour
{
    [SerializeField] private List<NPCConfiguration> npcConfigurations;
    [SerializeField] private float timer;
    [SerializeField] private Transform spawnPoint;
    private SpawnArmySystemMenu _spawnArmySystemMenu;
    private ArmySpawnConfiguration _armySpawnConfiguration;
    private ArmySystem _armySystem;
    private float _currentTimer;
    private NPCConfiguration _currentNPCConfig;
    private int _amountSpawn;
    private bool _initialized;

    private void Start()
    {
        _armySystem = ServiceLocator.GetService<ArmySystem>();
        _spawnArmySystemMenu = ServiceLocator.GetService<SpawnArmySystemMenu>();
        if (GetComponent<BuildingContractor>().IsMine == false)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (_currentTimer == 0 || _initialized == false) return;
        if (_currentTimer < 0)
        {
            _currentTimer = 0;
            if (_armySpawnConfiguration.IsAutoSpawn)
            {
                Spawn();
            }

            if (_amountSpawn > 0)
            {
                Spawn();
            }
        }
        else
        {
            _currentTimer -= Time.deltaTime;
        }
    }

    public void OnClick()
    {
        _spawnArmySystemMenu.Initialize(npcConfigurations,this);
    }

    public void StartSpawn()
    {
        _amountSpawn += _armySpawnConfiguration.AmountEnemyOnSpawn;
    }

    public void Initialize(ArmySpawnConfiguration armySpawnConfiguration)
    {
        _initialized = true;
       _currentTimer = timer;
        _armySpawnConfiguration = armySpawnConfiguration;
    }
    
    private void Spawn()
    {
        if (_currentTimer != 0)
        {
            return;
        }

        _currentTimer = timer;
        if (IsHaveNPC())
        {
            var newGameObject = PhotonNetwork.Instantiate(_currentNPCConfig.NPCGameObject.name, spawnPoint.position, spawnPoint.rotation);
            newGameObject.GetComponent<PhotonView>()
                .RPC(RPCEvents.SpawnNPC.ToString(), RpcTarget.All, PhotonNetwork.LocalPlayer.UserId);
            _armySystem.RemoveNpcInArmy(_currentNPCConfig, 1);
            if (_armySpawnConfiguration.IsAutoSpawn == false)
            {
                _amountSpawn--;
            }
            Debug.Log($"Spawn new NPC {newGameObject.name}");
        }
    }

    private bool IsHaveNPC()
    {
        var returnValue = false;
        foreach (var npc in npcConfigurations)
        {
            if (_armySystem.Compare(npc, 1))
            {
                returnValue = true;
                _currentNPCConfig = npc;
                break;
            }
        }

        return returnValue;
    }
}