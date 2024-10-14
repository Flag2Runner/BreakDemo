using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[self] attack [target]", category: "Action", id: "dbd6da55b9bce9f07e357279b684035b")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        IBehaviorInterface selfInterface = Self.Value.GetComponent<IBehaviorInterface>();
        if (selfInterface is not null)
        {
            selfInterface.Attack(Target.Value);
        }
        return Status.Success;
    }
}

