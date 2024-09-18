using UnityEngine;

[RequireComponent(typeof(AimingComponent))]
public class Revolver : Weapon
{
    [SerializeField] private float damage = 5;
    private AimingComponent _aimingComponent;

    private void Awake()
    {
        _aimingComponent = GetComponent<AimingComponent>();
    }

    public override void Attack()
    {
        /*GameObject target = _aimingComponent.GetAimTarget(Owner.transform);
        if (target)
        {
            HealthComponent targetHealthComponent = target.GetComponent<HealthComponent>();
            targetHealthComponent?.ChangeHealth(-damage);
        }*/
    }
}
