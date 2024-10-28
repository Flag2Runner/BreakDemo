using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilitySystemComponent : MonoBehaviour
{
    public delegate void OnAbilityGivenDelegate(Ability newAbility);
    public event OnAbilityGivenDelegate onAbilityGiven;

    public delegate void OnManaUpdaedDelegate(float newMana, float delta, float maxMana);
    public event OnManaUpdaedDelegate onManaUpdaed;
    
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private Ability[] initialAbilities;

    private List<Ability> _abilities = new List<Ability>();
    private float _mana;

    public float Mana
    {
        get => _mana;
        private set => _mana = value;
    }

    public float MaxMana
    {
        get => maxMana;
        private set => maxMana = value;
    }

    private void Awake()
    {
        _mana = maxMana;
    }

    private void Start()
    {
        _mana = maxMana;
        foreach (Ability ability in initialAbilities)
        {
            GiveAbility(ability);
        }
        //onManaUpdaed?.Invoke(_mana, maxMana, maxMana);
    }

    public void GiveAbility(Ability newAbility)
    {
        Ability ability = Instantiate(newAbility);
        ability.Init(this);
        
        _abilities.Add(ability);
        onAbilityGiven?.Invoke(ability);
    }

    public bool TryConsumeMana(float manaCost)
    {
        if (_mana < manaCost)
        {
            return false;
        }

        _mana -= manaCost;
        onManaUpdaed?.Invoke(_mana, -manaCost, maxMana);
        return true;
    }
}
