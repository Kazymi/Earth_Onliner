using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServicesInstaller : MonoBehaviour
{
    [SerializeField] private DeadNPCManager deadNpcManager;
    [SerializeField] private AmmoManager ammoManager;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        ServiceLocator.Subscribe<GameManager>(gameManager);
        ServiceLocator.Subscribe<BuildingManager>(buildingManager);
        ServiceLocator.Subscribe<InputHandler>(inputHandler);
        ServiceLocator.Subscribe<DeadNPCManager>(deadNpcManager);
        ServiceLocator.Subscribe<AmmoManager>(ammoManager);
    }
}
