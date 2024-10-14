using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "PatrolPointsEmpty", story: "[PatrolPoint] Is Empty [Condition]", category: "Conditions", id: "9ebf271ca193bbcf4b32c23cbfa43ab9")]
public partial class PatrolPointsEmptyCondition : Condition
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> PatrolPoint;
    [SerializeReference] public BlackboardVariable<bool> Condition;

    public override bool IsTrue()
    {
        bool bIsPatrolPointsEmpty = PatrolPoint.Value.Count == 0;
        return bIsPatrolPointsEmpty == Condition.Value;
    }
}
