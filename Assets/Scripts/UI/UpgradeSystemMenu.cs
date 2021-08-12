using System;
using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystemMenu : MonoBehaviour, IChangingAmountResources
{
    [SerializeField] private Canvas upgradeCanvas;
    [SerializeField] private TMP_Text priceWood;
    [SerializeField] private TMP_Text priceIron;
    [SerializeField] private TMP_Text priceGold;
    [SerializeField] private TMP_Text descriptionUpgrade;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button closeCanvasButton;

    private UpgradeSystem _upgradeSystem;

    public void NewUpgrade(UpgradePrice upgrade, Upgrader upgrader, string description)
    {
        upgradeButton.onClick.RemoveAllListeners();
        upgradeCanvas.enabled = true;
        _upgradeSystem = new UpgradeSystem(upgrade, upgrader);
        UpdateState(upgrade, description);
        upgradeButton.onClick.AddListener(_upgradeSystem.Upgrade);
    }

    private void UpdateState(UpgradePrice upgrade, string description)
    {
        priceWood.text = upgrade.NeedWood.ToString();
        priceGold.text = upgrade.NeedGold.ToString();
        priceIron.text = upgrade.NeedIron.ToString();
        descriptionUpgrade.text = description;
    }

    private void OnEnable()
    {
        if (_upgradeSystem != null)
            upgradeButton.onClick.AddListener(_upgradeSystem.Upgrade);
        closeCanvasButton.onClick.AddListener(() => upgradeCanvas.enabled = false);
        ServiceLocator.Subscribe<UpgradeSystemMenu>(this);
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        if (_upgradeSystem != null)
            upgradeButton.onClick.RemoveListener(_upgradeSystem.Upgrade);
        closeCanvasButton.onClick.RemoveListener(() => upgradeCanvas.enabled = false);
        ServiceLocator.Unsubscribe<UpgradeSystemMenu>();
        EventBus.Unsubscribe(this);
    }

    public void CloseCanvas()
    {
        upgradeCanvas.enabled = false;
    }

    public void ChangingAmountResources(TypeResource typeResource, int amount)
    {
        if (_upgradeSystem != null)
        {
            upgradeButton.interactable = _upgradeSystem.IsUnlockUpgrade();
        }
    }
}