using UnityEngine;

[RequireComponent(typeof(AimingComponent))]
public class RangedWeapon : Weapon
{
    [SerializeField] private float damage = 5;
    [SerializeField] private ParticleSystem bulletVFX;
    
    private AimingComponent _aimingComponent;

    private void Awake()
    {
        _aimingComponent = GetComponent<AimingComponent>();
    }

    public override void Attack()
    {
        AimResult aimResult = _aimingComponent.GetAimResult();
        if (aimResult.target)
        {
            HealthComponent targetHealthComponent = aimResult.target.GetComponent<HealthComponent>();
            targetHealthComponent?.ChangeHealth(-damage, Owner);
        }

        bulletVFX.Emit(bulletVFX.emission.GetBurst(0).maxCount);
        bulletVFX.transform.position = aimResult.aimStart;
        bulletVFX.transform.forward = aimResult.aimDir;
    }
}
