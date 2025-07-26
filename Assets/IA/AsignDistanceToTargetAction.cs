using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AsignDistanceToTarget", story: "Check distance between [Self] and [Target] and save it into [DistanceToTarget]", category: "Action", id: "1a2aeb4716bebbc311d71247c6b2bfee")]
public partial class AsignDistanceToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> DistanceToTarget;

    protected override Status OnStart()
    {
        return Status.Running;
    }
    
    protected override Status OnUpdate()
    {
        DistanceToTarget.Value = Vector2.Distance(
            (Vector2)Self.Value.transform.position,
            (Vector2)Target.Value.transform.position
            );
        // Self.Value.transform.rotation = Quaternion.Euler(0, 0, 0);
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

