using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Navigate2D", story: "2D [Self] navigates to [Target]", category: "Action", id: "98a387c1de5bcc2bdb15de58b359d3c4")]
public partial class Navigate2DAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Speed = new BlackboardVariable<float>(1.0f);
    [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new BlackboardVariable<float>(0.1f);

    protected override Status OnStart()
    {
        if (Self.Value == null || Target.Value == null)
            return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Self.Value == null || Target.Value == null)
            return Status.Failure;

        Transform SelfTransform = Self.Value.transform;
        Transform targetTransform = Target.Value.transform;

        Vector2 SelfPos = SelfTransform.position;
        Vector2 targetPos = targetTransform.position;

        float distance = Vector2.Distance(SelfPos, targetPos);

        if (distance <= DistanceThreshold)
            return Status.Success;

        // Move toward target
        Vector2 direction = (targetPos - SelfPos).normalized;
        Vector2 newPosition = Vector2.MoveTowards(SelfPos, targetPos, Speed * Time.deltaTime);
        SelfTransform.position = new Vector3(newPosition.x, newPosition.y, 0);

        // Flip sprite depending on direction
        SpriteRenderer sr = Self.Value.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = direction.x < 0; // flip left if moving left
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}


