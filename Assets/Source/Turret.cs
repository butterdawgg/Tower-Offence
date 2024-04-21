using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Turret : MonoBehaviour
{
    [SerializeField] protected Transform turretBase;
    [SerializeField] protected Transform turretPivot;
    [SerializeField] protected Transform muzzlePoint;
    [SerializeField] protected Projectile projectile;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected float damage;
    [SerializeField] protected float radius;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float projectileSpeed;

    protected List<Transform> targets;

    protected float lastShotTime = 0f;

    private void Awake()
    {
        GetComponent<SphereCollider>().radius = radius;

        targets = new List<Transform>();
    }

    private void Update()
    {
        if (targets.Count == 0)
            return;

        foreach (Transform t in targets)
        {
            if (t == null)
                targets.Remove(t);
        }

        foreach (Transform t in targets)
        {
            if (!t.gameObject.activeInHierarchy)
                targets.Remove(t);
        }

        Transform closestTarget = targets[0];

        foreach (Transform t in targets)
        {
            if ((closestTarget.position - transform.position).magnitude > (t.position - transform.position).magnitude)
                closestTarget = t;
        }

        if (closestTarget == null)
            return;

        if ((transform.position - closestTarget.position).magnitude > radius + 1f)
        {
            targets.Remove(closestTarget);

            return;
        }

        turretBase.LookAt(closestTarget.position, Vector3.up);
        turretBase.localEulerAngles = new Vector3(0f, turretBase.localEulerAngles.y, 0f);

        turretPivot.LookAt(closestTarget.position, Vector3.up);
        turretPivot.localEulerAngles = new Vector3(turretPivot.localEulerAngles.x, 0f, 0f);

        if (lastShotTime < Time.time - cooldown & Vector3.Angle(muzzlePoint.forward, closestTarget.position - muzzlePoint.position) < 15f)
        {
            AudioManager.Instance.PlaySound("Fire");

            Projectile proj = Instantiate(projectile.gameObject, muzzlePoint.position, Quaternion.identity, default).GetComponent<Projectile>();
            proj.Launch(closestTarget, projectileSpeed, damage, layerMask);

            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollision(other);
    }

    protected abstract void OnCollision(Collider other);
}
