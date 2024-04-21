using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Projectile
{
    public override void Launch(Transform target, float speed, float damage, LayerMask layerMask)
    {
        transform.LookAt(target.position);

        velocity = transform.forward * speed;
        this.damage = damage;
        this.layerMask = layerMask;
    }

    protected override void OnCollision(RaycastHit hit)
    {
        if (hit.rigidbody == null)
            return;

        if (hit.rigidbody.gameObject.TryGetComponent(out Unit unit))
            unit.Health -= damage;
    }
}