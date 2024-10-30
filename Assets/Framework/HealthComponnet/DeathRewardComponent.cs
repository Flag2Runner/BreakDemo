using UnityEngine;

public abstract class Reward : ScriptableObject
{
    public abstract void ApplyReward(GameObject target);
}
[CreateAssetMenu(menuName = "Rewards/Health Reward")]

public class HealthReward : Reward
{
    [SerializeField] private float healthRewardAmt = 20f;
    public override void ApplyReward(GameObject target)
    {
        HealthComponent healthComponent = target.GetComponent<HealthComponent>();
        if(healthComponent != null)
        {
            healthComponent.ChangeHealth(healthRewardAmt, target);
        }
    }
}
[CreateAssetMenu(menuName = "Rewards/Mana Reward")]

public class ManaReward : Reward
{
    [SerializeField] private float manaRewardAmt = 20f;
    public override void ApplyReward(GameObject target)
    {
        AbilitySystemComponent abilitySystem = target.GetComponent<AbilitySystemComponent>();
        if(abilitySystem != null)
        {
            abilitySystem.ChangeMana(manaRewardAmt);
        }
    }
}

[CreateAssetMenu(menuName = "Rewards/Credit Reward")]

public class CreditReward : Reward
{
    [SerializeField] private int rewardAmt = 20;
    public override void ApplyReward(GameObject target)
    {
        CreditComponent creditComponent = target.GetComponent<CreditComponent>();
        if(creditComponent != null)
        {
            creditComponent.ChangeCredit(rewardAmt);
        }
    }
}
public class DeathRewardComponent : MonoBehaviour
{
    [SerializeField] private Reward[] rewards;
    private void Awake()
    {
        HealthComponent _healthComponent = GetComponent<HealthComponent>();
        if(_healthComponent != null)
        {
            _healthComponent.OnDead += RewardKiller;
        }
    }

    private void RewardKiller(GameObject killer)
    {
        foreach(Reward r in rewards)
        {
            r.ApplyReward(killer);
        }
    }
}


