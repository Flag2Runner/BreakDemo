using UnityEngine;

public class SightSense : Sense
{
    [SerializeField] private float sightRange = 3f;
    [SerializeField] private float sightPeripheralHalfAngle = 45f;
    protected override bool IsStimuliSensible(Stimuli stimuli)
    {
        if (!transform.InRangeOf(stimuli.transform, sightRange))
            return false;

        if (!transform.InAngleOf(stimuli.transform, sightPeripheralHalfAngle))
            return false;

        if (transform.IsBlockedTo(stimuli.transform, Vector3.up, sightRange))
            return false;
        
        return true;
    }

    protected override void OnDrawDebug()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up, sightRange);

        Vector3 lineLeft = Quaternion.AngleAxis(sightPeripheralHalfAngle, Vector3.up) * transform.forward;
        Vector3 lineRight = Quaternion.AngleAxis(-sightPeripheralHalfAngle, Vector3.up) * transform.forward;
        
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + lineLeft * sightRange);    
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + lineRight * sightRange);    
    }
}
