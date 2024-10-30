using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditBar : Widget
{
    [SerializeField] private Button shopButton;
    [SerializeField] private TextMeshProUGUI creditsText;

    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        CreditComponent ownerCreditComponent = newOwner.GetComponent<CreditComponent>();
        if (ownerCreditComponent != null )
        {
            ownerCreditComponent.OnCrditChange += (newCredits) => creditsText.text = $"{newCredits}";
            creditsText.text = $"{ownerCreditComponent.Credits}";
        }
    }
    private void Awake()
    {
        shopButton.onClick.AddListener(SwitchToShop);
    }

    private void SwitchToShop()
    {
        GameplayWidget.Instance.SwitchToShop();
    }
}
