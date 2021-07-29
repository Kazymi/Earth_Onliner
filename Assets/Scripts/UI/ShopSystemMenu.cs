using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystemMenu : MonoBehaviour
{
    [SerializeField] private List<BuyConfiguration> buyConfigurations;
    [SerializeField] private Button openShopButton;
    [SerializeField] private Button closeShopButton;
    [SerializeField] private Button nextShopButton;
    [SerializeField] private Button lastShopButton;
    [SerializeField] private BuySystemMenu buyPrefab;
    [SerializeField] private Canvas shopCanvas;
    
    private List<Canvas> _buySystemCanvases;
    private int _currentIdCanvas;
    private ShopSystem _shopSystem;

    private void OnEnable()
    {
        openShopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);
        nextShopButton.onClick.AddListener(NextShop);
        lastShopButton.onClick.AddListener(LastShop);
    }

    private void OnDisable()
    {
        openShopButton.onClick.RemoveListener(OpenShop);
        closeShopButton.onClick.RemoveListener(CloseShop);
        nextShopButton.onClick.RemoveListener(NextShop);
        lastShopButton.onClick.RemoveListener(LastShop);
    }

    private void Start()
    {
        _buySystemCanvases = new List<Canvas>();
        _shopSystem = new ShopSystem(buyConfigurations.Count - 1);
        foreach (var buyConfiguration in buyConfigurations)
        {
            var newBuySystem = Instantiate(buyPrefab, shopCanvas.transform);
            newBuySystem.Initialize(this,buyConfiguration);
            var canvas = newBuySystem.GetComponent<Canvas>();
            _buySystemCanvases.Add(canvas);
            canvas.enabled = false;
        }

        _currentIdCanvas = 0;
        _buySystemCanvases[0].enabled = true;
    }

    public void CloseShop()
    {
        shopCanvas.enabled = false;
        openShopButton.interactable = true;
    }
    
    private void OpenShop()
    {
        shopCanvas.enabled = true;
        openShopButton.interactable = false;
    }

    private void NextShop()
    {
        _buySystemCanvases[_currentIdCanvas].enabled = false;
        _currentIdCanvas = _shopSystem.NextShop();
        _buySystemCanvases[_currentIdCanvas].enabled = true;
    }

    private void LastShop()
    {
        _buySystemCanvases[_currentIdCanvas].enabled = false;
        _currentIdCanvas = _shopSystem.LastShop();
        _buySystemCanvases[_currentIdCanvas].enabled = true;
    }
}
