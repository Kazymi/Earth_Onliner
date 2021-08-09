using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField] private LayerMask buildMask;
    [SerializeField] private LayerMask earthMask;
    private StateMachine _stateMachine;

    private bool _isBuild;
    private bool _isUpgrade;
    private BuildState _buildState;
    private InputHandler _inputHandler;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<PlayerSystem>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<PlayerSystem>();
    }

    private void Start()
    {
        var inputHandler = ServiceLocator.GetService<InputHandler>();
        _inputHandler = inputHandler;
        var emptyState = new EmptyState();
        _buildState = new BuildState(inputHandler,buildMask,earthMask);
        var upgradeState = new UpgradeState(inputHandler,buildMask);

        _stateMachine = new StateMachine(emptyState);

        emptyState.AddTransition(_buildState, () => _isBuild && _isUpgrade == false);
        emptyState.AddTransition(upgradeState, () => _isBuild == false && _isUpgrade);

        _buildState.AddTransition(upgradeState, () => _isBuild == false && _isUpgrade);
        _buildState.AddTransition(emptyState, () => _isBuild == false && _isUpgrade == false);

        upgradeState.AddTransition(_buildState, () => _isBuild && _isUpgrade == false);
        upgradeState.AddTransition(emptyState, () => _isBuild == false && _isUpgrade == false);
    }

    private void Update()
    {
        _stateMachine.Tick();
        // TODO: should be handled by InputHandler
        if (Input.GetMouseButton(0))
        {
            _stateMachine.MouseDrag();
        }
    }

    public void BuildComplete()
    {
        _isBuild = false;
        _isUpgrade = true;
    }

    public void StartBuild(BuildingContractor buildGameObject)
    {
        _buildState.Initialize(buildGameObject);
        _isBuild = true;
        _isUpgrade = false;
    }
}