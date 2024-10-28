using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Health Regen")]
public class HealthRegenAbillity : Ability
{
    [SerializeField] private float regenAmt = 20f;
    [SerializeField] private float regenDuration = 3f;
    
    HealthComponent _ownerHealthComponent;

    public override void Init(AbilitySystemComponent abilitySystemComponent)
    {
        base.Init(abilitySystemComponent);
        _ownerHealthComponent = abilitySystemComponent.GetComponent<HealthComponent>();
        _ownerHealthComponent.OnHealthChanged += (_, _, _, _) => BrocastCanCast();
    }

    public override bool CanCast()
    {
        return base.CanCast() && !Mathf.Approximately(_ownerHealthComponent.GetHealth(), _ownerHealthComponent.GetMaxHealth()); 
    }

    protected override void ActivateAbility()
    {
        if (!CommitAbility())
        {
            return;
        }

        OwnerAsc.StartCoroutine(HealthRegenCoroutine());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator HealthRegenCoroutine()
    {
        float counter = 0f;
        float regenRate = regenAmt / regenDuration;
        while (counter < regenDuration)
        {
            counter += Time.deltaTime;
            _ownerHealthComponent.ChangeHealth(regenRate * Time.deltaTime, OwnerAsc.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }
}
