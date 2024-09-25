using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    public delegate void OnPerceptionTargetUpdatedDelegate(GameObject target, bool bIsSensed);

    public event OnPerceptionTargetUpdatedDelegate OnPerceptionTargetUpdated;
    
    private LinkedList<Stimuli> _currentSensedStimuliList = new();
    private Stimuli _currentTargetStimuli;
    
    private void Awake()
    {
        Sense[] senses = GetComponents<Sense>();
        foreach (Sense sense in senses)
        {
            sense.OnSenseUpdated += HandleSenseUpdated;
        }
    }

    private void HandleSenseUpdated(Stimuli stimuli, bool bWasSensed)
    {
        LinkedListNode<Stimuli> foundNode = _currentSensedStimuliList.Find(stimuli);
        if (bWasSensed)
        {
            if (foundNode != null)
            {
                _currentSensedStimuliList.AddAfter(foundNode, stimuli);
            }
            else
            {
                _currentSensedStimuliList.AddLast(stimuli);
            }
            
        }
        else
        {
            if (foundNode != null)
            {
                _currentSensedStimuliList.Remove(foundNode);
            }
        }

        DetermineTarget();

    }

    private void DetermineTarget()
    {
        if (_currentSensedStimuliList.Count == 0)
        {
            if (_currentTargetStimuli)
            {
                OnPerceptionTargetUpdated?.Invoke(_currentTargetStimuli.gameObject, false);
                _currentTargetStimuli = null;
            }
        }
        else
        {
            Stimuli earliestStimuli = _currentSensedStimuliList.First.Value;
            if (earliestStimuli != _currentTargetStimuli && earliestStimuli)
            {
                _currentTargetStimuli = earliestStimuli;
                OnPerceptionTargetUpdated?.Invoke(_currentTargetStimuli.gameObject, true);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_currentSensedStimuliList.Count == 0)
            return;

        Vector3 currentTargetStimuliLoc = _currentTargetStimuli.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentTargetStimuliLoc + Vector3.up, .7f);
        Gizmos.DrawLine(transform.position + Vector3.up, currentTargetStimuliLoc + Vector3.up);
        
        Gizmos.color = Color.white;

        foreach (Stimuli otherStimuli in _currentSensedStimuliList)
        {
            if (otherStimuli == _currentTargetStimuli)
                continue;

            Vector3 stimuliLoc = otherStimuli.transform.position;
            Gizmos.DrawSphere(stimuliLoc + Vector3.up, 0.5f);
            Gizmos.DrawLine(transform.position+ Vector3.up, stimuliLoc + Vector3.up);

        }
    }
}
