using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sense : MonoBehaviour
{
    public delegate void OnSenseUpdatedDelegate(Stimuli stimuli, bool bWasSensed);
    public event OnSenseUpdatedDelegate OnSenseUpdated;
    
    [SerializeField] private bool bDrawDebug = true;
    [SerializeField] private float forgetTime = 3f;
    
    private static HashSet<Stimuli> _registeredStimuliSet = new HashSet<Stimuli>();

    private HashSet<Stimuli> _currentSensibleStimuliSet = new HashSet<Stimuli>();

    private Dictionary<Stimuli, Coroutine> _forgettingCoroutines = new Dictionary<Stimuli, Coroutine>();

    public static void RegisterStimuli(Stimuli stimuli)
    {
        _registeredStimuliSet.Add(stimuli);
    }

    public static void UnRegisterStimuli(Stimuli stimuli)
    {
        _registeredStimuliSet.Remove(stimuli);
    }

    protected virtual bool IsStimuliSensible(Stimuli stimuli)
    {
        return false;
    }
    
    private void Update()
    {
        foreach(Stimuli stimuli in _registeredStimuliSet)
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
        // we can't sense it now, but also we did not sense it before, noting needs to be done
        if (!_currentSensibleStimuliSet.Contains(stimuli))
            return;

        _currentSensibleStimuliSet.Remove(stimuli);
        
        Coroutine forgetingCoroutine = StartCoroutine(ForgetStimuli(stimuli));
        _forgettingCoroutines.Add(stimuli,forgetingCoroutine);
    }

    private IEnumerator ForgetStimuli(Stimuli stimuli)
    {
        yield return new WaitForSeconds(forgetTime);
        _forgettingCoroutines.Remove(stimuli);
        OnSenseUpdated?.Invoke(stimuli, false);
    }

    public void HandleSensibleStimuli(Stimuli stimuli)
    {
        // we can sense it now, but we also can sense it before, nothing needs to be done
        if (_currentSensibleStimuliSet.Contains(stimuli))
            return;

        _currentSensibleStimuliSet.Add(stimuli);
        
        if (_forgettingCoroutines.ContainsKey(stimuli))
        {
            StopCoroutine(_forgettingCoroutines[stimuli]);
            _forgettingCoroutines.Remove(stimuli);
            return;
        }

        OnSenseUpdated?.Invoke(stimuli, true);
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
       //override in child class to draw debug 
    }
}
