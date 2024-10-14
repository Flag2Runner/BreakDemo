using System;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CooldownAction", story: "Cooldown after [durration] seconds", category: "Action", id: "ecbed927f1fae99c5550b926f24fbfc9")]
public partial class CooldownActionModifier : Modifier
{
    [SerializeReference] public BlackboardVariable<float> Durration;

    private float _lastExecutionTime = -1;

    protected override Status OnStart()
    {
        float currentTime = Time.timeSinceLevelLoad;
        if (_lastExecutionTime < 0 || currentTime - _lastExecutionTime > Durration.Value)
        {
            _lastExecutionTime = currentTime;
            return StartNode(Child);
        }
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Child.CurrentStatus;
    }
}

