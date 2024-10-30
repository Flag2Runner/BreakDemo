using System.Collections.Generic;
using UnityEngine;

public interface iPurchaseListener { public bool handlePurchase(Object newPurchase); }
public class CreditComponent : MonoBehaviour
{
    [SerializeField] int credits = 100;
    public int Credits
    {
        get => credits;
        private set => credits = value;
    }

    public delegate void OnCrditChangeDelegate(int newCredit);
    public event OnCrditChangeDelegate OnCrditChange;

    List<iPurchaseListener> listeners = new List<iPurchaseListener>();

    private void Awake()
    {
        foreach (iPurchaseListener listener in GetComponents<iPurchaseListener>())
        {
            listeners.Add(listener);

        }
    }

    private void BroadcastPurchase(Object item)
    {
        foreach (iPurchaseListener listener in listeners)
        {
            if(listener.handlePurchase(item))
                return;
        }
    }

    public bool Purchase(int price, Object item)
    {
        if(Credits < price)
        {
            return false;
        }
        ChangeCredit(-price);
        BroadcastPurchase(item);

        return true;

    }

    public void ChangeCredit(int rewardAmount)
    {
        Credits += rewardAmount;
        if(Credits <= 0)
        {
            Credits = 0;
        }

        OnCrditChange?.Invoke(Credits);
    }
}
