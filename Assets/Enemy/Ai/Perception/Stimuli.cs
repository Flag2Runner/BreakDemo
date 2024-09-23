using System;
using UnityEngine;

public class Stimuli : MonoBehaviour
{
    private void Start()
    {
        Sense.RegisterStimuli(this);
    }

    private void OnDestroy()
    {
        Sense.UnRegisterStimuli(this);
    }
}
