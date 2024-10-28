using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public delegate void OnAbilityCooldownStartedDelegate(float cooldownDuration);

    public delegate void OnAbilityCanCastChangedDelegate(bool bCanCast);

    public event OnAbilityCanCastChangedDelegate OnAbilityCanCastChanged;

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

    protected void BrocastCanCast()
    {
        OnAbilityCanCastChanged?.Invoke(CanCast());
    }

    public Sprite GetAbilityIcon()
    {
        return abilityIcon;
    }

    protected abstract void ActivateAbility();

    public bool TryActiveateAbility()
    {
        if (!CanCast())
        {
            return false;
        }
        ActivateAbility();
        return true;
    }

    public virtual bool CanCast()
    {
        return !_bIsOnCooldown && OwnerAsc.Mana >= manaCost;
    }

    public virtual void Init(AbilitySystemComponent abilitySystemComponent)
    {
        OwnerAsc = abilitySystemComponent;
        OwnerAsc.onManaUpdaed += (mana, delta, maxMana) => BrocastCanCast();
    }
    
    private void StartCooldown()
    {
        OnAblityCooldownStarted?.Invoke(cooldownDuration);
        OwnerAsc.StartCoroutine(CooldownCoroutine());
        BrocastCanCast();
    }

    IEnumerator CooldownCoroutine()
    {
        _bIsOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        _bIsOnCooldown = false;
        BrocastCanCast();
    }
    
    //triggers cooldown and consumes mana and tells you if your able to commit to begin with..
    protected bool CommitAbility()
    {
        if (!OwnerAsc)
            return false;
        
        if (_bIsOnCooldown)
            return false;
        
        if (!OwnerAsc.TryConsumeMana(manaCost))
            return false;
        
        StartCooldown();
        
        return true;
    }
}
