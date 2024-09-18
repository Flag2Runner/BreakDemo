using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void OnHealthChangeDelegate(float newHealth, float delta, float maxHealth);

    public event OnHealthChangeDelegate OnHealthChanged;
    public event OnHealthChangeDelegate OnTakenDamage;
    public event Action OnDead;
    
    [SerializeField] private float maxHealth = 100;
    private float _health;

    private void Awake()
    {
        _health = maxHealth;
    }

    public void ChangeHealth(float amt)
    {
        if(amt == 0 || _health == 0)
            return;

        _health += amt;
        _health = Mathf.Clamp(_health + amt, 0, maxHealth);

        if (amt < 0)
        {
            OnTakenDamage?.Invoke(_health, amt, maxHealth);
        }
        OnHealthChanged?.Invoke(_health, amt, maxHealth);

        if (_health <= 0)
        {
            OnDead?.Invoke();
        }
    }
}
