using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI; 

public class PlayerManaBar : Widget
{
    [SerializeField] private Image manaBarImage;
    [SerializeField] private TextMeshProUGUI valueText;
    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        AbilitySystemComponent ownerAsc = newOwner.GetComponent<AbilitySystemComponent>();
        if (ownerAsc)
        {
            ownerAsc.onManaUpdaed += UpdateMana;
            UpdateMana(ownerAsc.Mana, 0, ownerAsc.MaxMana);
        }
    }

    private void UpdateMana(float newMana, float delta, float maxMana)
    {
        manaBarImage.fillAmount = newMana / maxMana;
        valueText.text = $"{newMana:f0}/{maxMana:f0}";
    }
}
