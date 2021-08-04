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
    [SerializeField] private TMP_Text addWood;
    [SerializeField] private TMP_Text addIron;
    [SerializeField] private TMP_Text addGold;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button closeCanvasButton;

    private UpgradeSystem _upgradeSystem;

    public void NewUpgrade(Upgrade upgrade, GeneratorResource generatorResource, Upgrader upgrader)
    {
        if (_upgradeSystem != null)
            upgradeButton.onClick.RemoveListener(_upgradeSystem.Upgrade);
        upgradeCanvas.enabled = true;
        _upgradeSystem = new UpgradeSystem(upgrade, generatorResource, upgrader);
        UpdateState(upgrade);
        upgradeButton.onClick.AddListener(_upgradeSystem.Upgrade);
    }

    private void UpdateState(Upgrade upgrade)
    {
        priceWood.text = upgrade.NeedWood.ToString();
        priceGold.text = upgrade.NeedGold.ToString();
        priceIron.text = upgrade.NeedIron.ToString();

        addWood.text = upgrade.NewWood.GenerationResource.Amount.ToString();
        addIron.text = upgrade.NewIron.GenerationResource.Amount.ToString();
        addGold.text = upgrade.NewGold.GenerationResource.Amount.ToString();
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

    public void ChangingAmountResources(TypeResource typeResource)
    {
        if (_upgradeSystem != null)
        {
            upgradeButton.interactable = _upgradeSystem.IsUnlockUpgrade();
        }
    }
}