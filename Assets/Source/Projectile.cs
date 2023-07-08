using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected LayerMask layerMask;
    protected Vector3 velocity;
    protected float gravity;
    protected float damage;

    private bool isDead;
    private ParticleSystem ps;
    private MeshRenderer mr;

    private void Awake()
    {
        Destroy(gameObject, 100f);

        ps = GetComponentInChildren<ParticleSystem>();
        mr = GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        if (isDead)
            return;

        if (Physics.Raycast(transform.position, velocity, out RaycastHit hit, (velocity * Time.deltaTime).magnitude, layerMask))
        {
            transform.position = hit.point;
            isDead = true;
            Destroy(mr);
            ps.transform.rotation = Quaternion.LookRotation(hit.normal);
            ps.Play();
            Destroy(gameObject, 1f);

            OnCollision(hit);

            return;
        }

        transform.position += velocity * Time.deltaTime;
    }

    public abstract void Launch(Transform target, float speed, float damage, LayerMask layerMask);
    protected abstract void OnCollision(RaycastHit hit);
}