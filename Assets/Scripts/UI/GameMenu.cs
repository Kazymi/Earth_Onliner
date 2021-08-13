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

    private Dictionary<TypeResource, TMP_Text> _changingResources;
    private PlayerResources _playerResources;

    private void Start()
    {
        _playerResources = ServiceLocator.GetService<PlayerResources>();
        _changingResources = new Dictionary<TypeResource, TMP_Text>();
        
        _changingResources.Add(TypeResource.Wood,
            woodText);
        
        _changingResources.Add(TypeResource.Iron,
            ironText);
        
        _changingResources.Add(TypeResource.Gold,
            goldText);
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
    
    public void ChangingAmountResources(TypeResource typeResource,int amount)
    {
        _changingResources[typeResource].text = amount.ToString();
    }
}