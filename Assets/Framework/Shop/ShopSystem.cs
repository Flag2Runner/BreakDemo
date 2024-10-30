using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[CreateAssetMenu(menuName ="Shop/ShopSyetem")]
public class ShopSystem : ScriptableObject
{
    [SerializeField] ShopItem[] shopItems;

    public ShopItem[] GetItems()  { return shopItems; } 

    public bool TryPurchases(ShopItem selectedItem, CreditComponent purchaser)
    {
        return purchaser.Purchase(selectedItem.price, selectedItem.item);
    }
}
