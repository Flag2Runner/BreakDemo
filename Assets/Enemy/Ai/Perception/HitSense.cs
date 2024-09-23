using UnityEngine;

public class HitSense : Sense
{
    protected override bool IsStimuliSensible(Stimuli stimuli)
    {
        return false;
    }
}
