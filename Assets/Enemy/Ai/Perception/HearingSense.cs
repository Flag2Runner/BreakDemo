using UnityEngine;

public class HearingSense : Sense
{
    [SerializeField] private float hearingMinVolume = 10f;
    
    public delegate void OnSoundEventSentDelgate(float volume, Stimuli stimuli);

    private static float _attenuation = 0.05f;

    public static event OnSoundEventSentDelgate OnSoundEventSent;
    
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
        float soundTravleDistance = Vector3.Distance(transform.position, stimuli.transform.position);
        float volumeAtOwner = volume - 20 * Mathf.Log(soundTravleDistance, 10) - _attenuation * soundTravleDistance;
        Debug.Log($"volume at owner is: {volumeAtOwner}");
        if (volumeAtOwner < hearingMinVolume)
            return;
        
        HandleSensibleStimuli(stimuli);
        
    }

}
