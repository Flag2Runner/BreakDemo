using System;
using UnityEngine;

public class HitSense : Sense
{
    private void Awake()
    {
        HealthComponent healthComp = GetComponent<HealthComponent>();
        if (healthComp)
        {
            healthComp.OnTakenDamage += HandleDamageEvent;
        }
    }

    private void HandleDamageEvent(float newHealth, float delta, float maxHealth, GameObject instigator)
    {
        Stimuli instigatorStimuli = instigator.GetComponent<Stimuli>();
        if(instigatorStimuli)
            HandleSensibleStimuli(instigatorStimuli); 
    }
}
