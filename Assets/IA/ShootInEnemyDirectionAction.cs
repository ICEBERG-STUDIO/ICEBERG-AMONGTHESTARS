using System;
using System.Collections;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShootInEnemyDirection", story: "Make [Bullet] spawn and go to [Target]", category: "Action",
    id: "12db1f717fe1287b39c4e322e5bcfbee")]
public partial class ShootInEnemyDirectionAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Bullet;
    [SerializeReference] public BlackboardVariable<float> BulletSpeed;
    [SerializeReference] public BlackboardVariable<float> shootCooldown;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    private Vector2 targetPosition;
    private float timeUntilNextShot;

    protected override Status OnStart()
    {
        targetPosition = Target.Value.transform.position;
        timeUntilNextShot = 0f;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        timeUntilNextShot -= Time.deltaTime;
        Shoot();
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    private void Shoot()
    {
        if (timeUntilNextShot <= 0f)
        {
            Vector2 dir = (targetPosition - (Vector2)Self.Value.transform.position).normalized;
            GameObject bulletInstance = UnityEngine.Object.Instantiate(
                Bullet.Value,
                Self.Value.transform.position,
                Quaternion.identity
            );

            Rigidbody2D rgbd2D = bulletInstance.GetComponent<Rigidbody2D>();
            if (rgbd2D != null)
            {
                rgbd2D.linearVelocity = dir * BulletSpeed.Value;
            }
            else
            {
                Debug.LogWarning("Bullet Instance doesn't have a Rigidbody2D");
            }

            timeUntilNextShot = shootCooldown.Value;
        }
    }
}