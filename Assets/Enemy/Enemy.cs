using System;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private Animator _animator;
    private static readonly int DeadId = Animator.StringToHash("Dead");

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.OnTakenDamage += TookDamage;
        _healthComponent.OnDead += StartDeath;
        _animator = GetComponent<Animator>();
    }

    private void StartDeath()
    {
        _animator.SetTrigger(DeadId);
    }

    public void DeathAnimationFinished()
    {
       Destroy(gameObject); 
    }
    private void TookDamage(float newHealth, float delta, float maxHealth)
    {
        Debug.Log($"I took {delta} amt of damage, health is now {newHealth}/{maxHealth}");
    }
}
