using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetDefaultEnemyPosition", story: "Set [DefaultPosition] thanks to [self] position", category: "Action", id: "bff7df82224b87b5eea69f71c08616fc")]
public partial class SetDefaultEnemyPositionAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector2> DefaultPosition;
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnStart()
    {
        DefaultPosition.Value = new Vector2(Self.Value.transform.position.x, Self.Value.transform.position.y);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

