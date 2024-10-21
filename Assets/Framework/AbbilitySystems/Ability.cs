using System.Collections;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public delegate void OnAbilityCooldownStartedDelegate(float cooldownDuration);

    public event OnAbilityCooldownStartedDelegate OnAblityCooldownStarted;
    
    [SerializeField] private float cooldownDuration = 3f;
    [SerializeField] private float manaCost = 10f;
    [SerializeField] private Sprite abilityIcon;
    private bool _bIsOnCooldown;
    protected AbilitySystemComponent OwnerAsc
    { 
        get;
        private set;
    }

    public Sprite GetAbilityIcon()
    {
        return abilityIcon;
    }

    public abstract void ActivateAbility();
    
    public virtual void Init(AbilitySystemComponent abilitySystemComponent)
    {
        OwnerAsc = abilitySystemComponent;
    }
    
    private void StartCooldown()
    {
        OnAblityCooldownStarted?.Invoke(cooldownDuration);
        OwnerAsc.StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        _bIsOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        _bIsOnCooldown = false;
    }
    
    //triggers cooldown and consumes mana and tells you if your able to commit to begin with..
    protected bool CommitAbility()
    {
        if (!OwnerAsc)
            return false;

        if (!OwnerAsc.TryConsumeMana(manaCost))
            return false;
        
        if (_bIsOnCooldown)
            return false;
        
        StartCooldown();
        
        return true;
    }
}
