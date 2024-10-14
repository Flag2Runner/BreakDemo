using System;
using UnityEngine;

public class HearingSense : Sense
{
    [SerializeField] private float hearingMinVolume = 10f;
    public delegate void OnSoundEventSentDelegate(float volume, Stimuli stimuli);

    private static float _attenuation = 0.05f;
    public static event OnSoundEventSentDelegate OnSoundEventSent;

    public static void SendSoundEvent(float volume, Stimuli stimuli)
    {
        OnSoundEventSent?.Invoke(volume, stimuli);
    }

    private void Awake()
    {
        OnSoundEventSent += HandleSoundEvent;
    }


    private void HandleSoundEvent(float volume, Stimuli stimuli)
    {
        float soundTravelDistance = Vector3.Distance(transform.position, stimuli.transform.position);
        float volumeAtOwner = volume - 20 * Mathf.Log(soundTravelDistance, 10) - _attenuation * soundTravelDistance;
        Debug.Log($"volume at owner is: {volumeAtOwner}");
        if (volumeAtOwner < hearingMinVolume)
            return;
        
        HandleSensibleStimuli(stimuli); 
    }
    private void OnDestroy()
    {
        OnSoundEventSent -= HandleSoundEvent;
    }
}
