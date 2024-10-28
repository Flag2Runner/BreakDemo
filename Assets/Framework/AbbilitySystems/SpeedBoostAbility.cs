using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Speed Boost")]
public class SpeedBoostAbility : Ability
{
    [SerializeField] private float boostAmt = 20f;
    [SerializeField] private float boostDuration = 3f;
    
    private MovementComponent _movementComponent;

    public override void Init(AbilitySystemComponent abilitySystemComponent)
    {
        base.Init(abilitySystemComponent);
        _movementComponent = abilitySystemComponent.GetComponent<MovementComponent>();
    }

    protected override void ActivateAbility()
    {
        if (CommitAbility() && _movementComponent != null)
        {
            OwnerAsc.StartCoroutine(BoostCorutine());
        }
    }

    IEnumerator BoostCorutine()
    {
        _movementComponent.MoveSpeed += boostAmt;
        yield return new WaitForSeconds(boostDuration);
        _movementComponent.MoveSpeed -= boostAmt;
    }
}
