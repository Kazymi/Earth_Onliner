using System;
using System.Collections.Generic;
using EventBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour, IChangingAmountResources
{
    [SerializeField] private Button buildButton;
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text ironText;
    [SerializeField] private TMP_Text goldText;

    private Dictionary<TypeResource, Action> _changingResources;
    private PlayerResources _playerResources;

    private void Start()
    {
        _playerResources = ServiceLocator.GetService<PlayerResources>();
        _changingResources = new Dictionary<TypeResource, Action>();
        
        _changingResources.Add(TypeResource.Wood,
            () => woodText.text = _playerResources.GetAmountResource(TypeResource.Wood).ToString());
        
        _changingResources.Add(TypeResource.Iron,
            () => ironText.text = _playerResources.GetAmountResource(TypeResource.Iron).ToString());
        
        _changingResources.Add(TypeResource.Gold,
            () => goldText.text = _playerResources.GetAmountResource(TypeResource.Gold).ToString());
    }

    private void OnEnable()
    {
        ServiceLocator.Subscribe<GameMenu>(this);
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<GameMenu>();
        EventBus.Unsubscribe(this);
    }

    public void BuildButtonSetState(bool unlocked)
    {
        buildButton.gameObject.SetActive(unlocked);
    }

    // TODO: can simplify by passing the amount of resources to add
    public void ChangingAmountResources(TypeResource typeResource)
    {
        _changingResources[typeResource]?.Invoke();
    }
}