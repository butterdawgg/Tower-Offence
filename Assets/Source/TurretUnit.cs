using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUnit : Turret
{
    protected override void OnCollision(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            target = enemy.transform;

            Debug.Log("Collision");
        }
    }
}
