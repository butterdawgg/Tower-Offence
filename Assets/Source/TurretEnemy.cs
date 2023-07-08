using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Turret
{
    protected override void OnCollision(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Unit unit))
            targets.Add(unit.transform);
    }
}
