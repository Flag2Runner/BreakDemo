using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sense : MonoBehaviour
{
    [SerializeField] private bool bDrawDebug = true;
    [SerializeField] private float forgetTime = 3f;
    private static HashSet<Stimuli> _registeredSimuliSet = new HashSet<Stimuli>();

    private HashSet<Stimuli> _currentSensibleStimuliSet = new HashSet<Stimuli>();

    private Dictionary<Stimuli, Coroutine> _forgettingCorutines = new Dictionary<Stimuli, Coroutine>();

    public static void RegisterStimuli(Stimuli stimuli)
    {
        _registeredSimuliSet.Add(stimuli);
    }

    public static void UnRegisterStimuli(Stimuli stimuli)
    {
        _registeredSimuliSet.Remove(stimuli);
    }


    protected abstract bool IsStimuliSensible(Stimuli stimuli);

    private void Update()
    {
        foreach (Stimuli stimuli in _registeredSimuliSet)
        {
            if (IsStimuliSensible(stimuli))
            {
                HandleSensibleStimuli(stimuli);
            }
            else
            {
                HandleNoSensibleStimuli(stimuli);
            }
        }
    }
    
    private void HandleNoSensibleStimuli(Stimuli stimuli)
    {
        // We can't sense it now, but also we did not sense it before, nothing needs to be done
        if(!_currentSensibleStimuliSet.Contains(stimuli))
            return;

        _currentSensibleStimuliSet.Remove(stimuli);
        Coroutine forgetingCorutine = StartCoroutine(ForgetStimuli(stimuli));
        _forgettingCorutines.Add(stimuli, forgetingCorutine);
    }

    private IEnumerator ForgetStimuli(Stimuli stimuli)
    {
        yield return new WaitForSeconds(forgetTime);
        _forgettingCorutines.Remove(stimuli);
        Debug.Log($"I just Lost: {stimuli.gameObject.name}");
    }

    private void HandleSensibleStimuli(Stimuli stimuli)
    {
        // we can sense it now, but we also can sense it before, nothing needs to be done
        if(_currentSensibleStimuliSet.Contains((stimuli)))
            return;

        _currentSensibleStimuliSet.Add(stimuli);

        if (_forgettingCorutines.ContainsKey(stimuli))
        {
            StopCoroutine(_forgettingCorutines[stimuli]);
            _forgettingCorutines.Remove(stimuli);
            return;
        }
        
        Debug.Log($"I just sensed: {stimuli.gameObject.name}");
    }

    private void OnDrawGizmos()
    {
        if (bDrawDebug)
        {
            OnDrawDebug();
        }
    }
    protected virtual void OnDrawDebug()
    {
        
    }
}
