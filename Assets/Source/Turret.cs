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

    protected Transform target;

    protected float lastShotTime = 0f;

    private void Awake()
    {
        GetComponent<SphereCollider>().radius = radius;
    }

    private void Update()
    {
        if (target == null)
            return;

        if ((transform.position - target.position).magnitude > radius + 1f)
        {
            target = null;

            turretBase.localEulerAngles = Vector3.zero;
            turretPivot.localEulerAngles = Vector3.zero;

            return;
        }

        turretBase.LookAt(target.position);
        turretBase.localEulerAngles = new Vector3(0f, turretBase.localEulerAngles.y, 0f);

        turretPivot.LookAt(target.position);
        turretPivot.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, 0f);

        if (lastShotTime < Time.time - cooldown & Vector3.Angle(muzzlePoint.forward, target.position - muzzlePoint.position) < 15f)
        {
            Projectile proj = Instantiate(projectile.gameObject, muzzlePoint.position, Quaternion.identity, default).GetComponent<Projectile>();
            proj.Launch(target, projectileSpeed, damage, layerMask);

            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollision(other);
    }

    protected abstract void OnCollision(Collider other);
}
