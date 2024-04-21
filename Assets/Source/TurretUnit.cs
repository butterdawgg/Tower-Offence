using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUnit : Turret
{
    protected override void OnCollision(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (!enemy.IsDead)
                targets.Add(enemy.transform);
        }
    }
}
