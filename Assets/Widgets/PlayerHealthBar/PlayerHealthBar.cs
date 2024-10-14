using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealthBar : Widget
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI valueText;
    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        HealthComponent ownerHealthComp = newOwner.GetComponent<HealthComponent>();
        if (ownerHealthComp)
        {
            ownerHealthComp.OnHealthChanged += UpdateHealth;
            UpdateHealth(ownerHealthComp.GetHealth(), 0, ownerHealthComp.GetMaxHealth(), newOwner);
        }
    }

    private void UpdateHealth(float newHealth, float delta, float maxHealth, GameObject instigator)
    {
        healthBarImage.fillAmount = newHealth / maxHealth;
        valueText.text = $"{newHealth}/{maxHealth}";
    }
}
