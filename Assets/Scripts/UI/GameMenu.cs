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

    private Dictionary<TypeResource, Action> _changingResourses;
    private PlayerResources _playerResources;

    private void Start()
    {
        _playerResources = ServiceLocator.GetService<PlayerResources>();
        _changingResourses = new Dictionary<TypeResource, Action>();
        
        _changingResourses.Add(TypeResource.Wood,
            () => woodText.text = _playerResources.GetAmountResource(TypeResource.Wood).ToString());
        
        _changingResourses.Add(TypeResource.Iron,
            () => ironText.text = _playerResources.GetAmountResource(TypeResource.Iron).ToString());
        
        _changingResourses.Add(TypeResource.Gold,
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

    public void ChangingAmountResources(TypeResource typeResource)
    {
        _changingResourses[typeResource]?.Invoke();
    }
}