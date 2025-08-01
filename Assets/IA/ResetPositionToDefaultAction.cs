using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ResetPositionToDefault", story: "Make [Self] travel to [defaultposition] and set [IaStateMachine] to patrol", category: "Action", id: "fa4f8a94259b1b889153cc6e91644c66")]
public partial class ResetPositionToDefaultAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector2> Defaultposition;
    [SerializeReference] public BlackboardVariable<IaStateMachine> IaStateMachine;
    [SerializeReference] public BlackboardVariable<float> speed;
    [SerializeReference] public BlackboardVariable<float> threshold;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Self?.Value == null)
            return Status.Failure;

        Vector2 currentPosition = Self.Value.transform.position;
        Vector2 targetPosition = Defaultposition.Value;
        Vector2 delta = currentPosition - targetPosition;

        // Move the object
        Self.Value.transform.position = Vector2.MoveTowards(
            currentPosition,
            targetPosition,
            speed *Time.maximumParticleDeltaTime
        );

        // Check distance
        if (Vector2.Distance(currentPosition, targetPosition) <= threshold)
        {
            IaStateMachine.Value = global::IaStateMachine.Patrol;
            // Update state machine here
            return Status.Success;
        }

        return Status.Running;
    }
    
    protected override void OnEnd()
    {
    }
}

