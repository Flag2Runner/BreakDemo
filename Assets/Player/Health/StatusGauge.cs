using UnityEngine;
using UnityEngine.Rendering;

public class StatusGauge : Widget
{
    [SerializeField] private ValueGauge healthBar;
    [SerializeField] private ValueGauge manaBar;

    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        HealthComponent ownerHealthComponent = newOwner.GetComponent<HealthComponent>();
        if (ownerHealthComponent)
        {
            
        }
    }
}
