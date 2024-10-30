using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ShopWidget : Widget
{
    [SerializeField] private ShopSystem shopSystem;
    [SerializeField] ShopItemWidget shopItemWidgetPrefab;
    [SerializeField] private RectTransform shopList;
    [SerializeField] private Button backButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI creditText;

    List<ShopItemWidget> _shopItemsWidget = new List<ShopItemWidget>();
    private CreditComponent _creditComponent;
    private ShopItemWidget currentSelectedShopItemWidget;

    private void Awake()
    {
        backButton.onClick.AddListener(SwitchOffShop);
        buyButton.onClick.AddListener(TryPurchase);
    }

    private void TryPurchase()
    {
        if(!currentSelectedShopItemWidget || !shopSystem.TryPurchases(currentSelectedShopItemWidget.GetItem(), _creditComponent))
            { return; }

        Destroy(currentSelectedShopItemWidget.gameObject);
        _shopItemsWidget.Remove(currentSelectedShopItemWidget);
        currentSelectedShopItemWidget = null;
    }

    private void SwitchOffShop()
    {
        currentSelectedShopItemWidget = null;
        GameplayWidget.Instance.SwitchToGameplay();
    }
    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        _creditComponent = newOwner.GetComponent<CreditComponent>();
        _creditComponent.OnCrditChange += UpdateCredit;
        UpdateCredit(_creditComponent.Credits);
        InitShopItems();
    }

    private void InitShopItems()
    {
        foreach(ShopItem shopItem in shopSystem.GetItems())
        {
            AddShopItem(shopItem);
        }
    }

    private void AddShopItem(ShopItem shopItem)
    {
        ShopItemWidget shopItemWidget = Instantiate(shopItemWidgetPrefab, shopList);
        shopItemWidget.Init(shopItem, _creditComponent.Credits);
        shopItemWidget.OnItemSelected += ItemSelected;
        _shopItemsWidget.Add(shopItemWidget);
    }

    private void ItemSelected(ShopItemWidget shopItem)
    {
        currentSelectedShopItemWidget = shopItem;
    }

    private void UpdateCredit(int newCredit)
    {
        creditText.text = $"{ newCredit}";
        RefreshShopItemWidget(newCredit);
    }

    private void RefreshShopItemWidget(int newCredit)
    {
        foreach(ShopItemWidget item in _shopItemsWidget)
        {
            item.Refresh(newCredit);
        }
    }
}
