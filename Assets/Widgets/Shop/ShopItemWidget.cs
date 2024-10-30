using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemWidget : MonoBehaviour
{
    public delegate void OnItemSelectedDelegate(ShopItemWidget shopItem);
    public event OnItemSelectedDelegate OnItemSelected;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI DescriptionText;

    [SerializeField] private Button Button;
    [SerializeField] private Image grayOutCover;

    [SerializeField] Color cannotPurchaseColor = Color.white;
    [SerializeField] Color canPurchaseColor = Color.gray;

    private ShopItem _item;

    private void Awake()
    {
        Button.onClick.AddListener(()  => OnItemSelected?.Invoke(this));

    }

    public void Init(ShopItem item, int avaliableCredits)
    {
        _item = item;
        icon.sprite = _item.itemIcon;
        titleText.text = item.title;
        DescriptionText.text = item.description;
        priceText.text = $"${item.price}";

        Refresh(avaliableCredits);
    }

    public void Refresh(int avaliableCredits)
    {
        bool bCanPurchase = avaliableCredits >= _item.price;
        grayOutCover.enabled = !bCanPurchase;
        priceText.color = bCanPurchase ? canPurchaseColor : cannotPurchaseColor;
    }

    public ShopItem GetItem()
    {
        return _item; 
    }
}
