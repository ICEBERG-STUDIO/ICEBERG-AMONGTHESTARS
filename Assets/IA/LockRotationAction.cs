using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LockRotation", story: "Lock [Self] rotation", category: "Action", id: "55e3c778e4cade42ad40d2c8915006f3")]
public partial class LockRotationAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Self.Value.transform.rotation = Quaternion.Euler(0, 0, 0);
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

