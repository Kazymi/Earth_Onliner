using System;
using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuySystemMenu : MonoBehaviour, IChangingAmountResources
{
    [SerializeField] private BuyConfiguration buyConfiguration;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text ironText;
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image image;
    [SerializeField] private Button buyButton;

    private BuySystem _buySystem;
    private BuyConfiguration _buyConfiguration;
    

    public void Initialize(ShopSystemMenu systemMenu,BuyConfiguration buyConfiguration)
    {
        _buyConfiguration = buyConfiguration;
        _buySystem = new BuySystem(_buyConfiguration,systemMenu);
        goldText.text = _buyConfiguration.NeedGold.ToString();
        ironText.text = _buyConfiguration.NeedIron.ToString();
        woodText.text = _buyConfiguration.NeedWood.ToString();
        descriptionText.text = _buyConfiguration.Description;
        image.sprite = _buyConfiguration.Icon;
        
        buyButton.interactable = _buySystem.IsUnlockedBuy();
        buyButton.onClick.AddListener(_buySystem.Buy);
    }
    
    private void OnEnable()
    {
        if (_buySystem != null)
        {
            buyButton.onClick.AddListener(_buySystem.Buy);
        }
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(_buySystem.Buy);
        EventBus.Subscribe(this);
    }

    public void ChangingAmountResources(TypeResource typeResource)
    {
        buyButton.interactable = _buySystem.IsUnlockedBuy();
    }
}