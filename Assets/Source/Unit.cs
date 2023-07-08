using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class Unit : MonoBehaviour
{
    [SerializeField] private SplineContainer sc;
    [SerializeField] private float speed;
    [SerializeField] private float minX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxX;
    [SerializeField] private float maxZ;

    private Rigidbody rb;

    private Vector3 position;
    private Vector3 forwardVector;
    private Vector3 upVector;
    private Vector3 rightVector;

    private float offsetX;
    private float offsetZ;
    private float offsetY;

    private bool levelStarted = false;

    public float Health { get; set; }

    private void Update()
    {
        if (levelStarted)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            OnLevelStart();

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, Mathf.Clamp(transform.position.z, minZ, maxZ));

        offsetX = transform.position.z;
        offsetZ = transform.position.x;
        offsetY = transform.position.y;
    }

    private void OnLevelStart()
    {
        StartCoroutine(OnLevelStartCoroutine());
    }

    private IEnumerator OnLevelStartCoroutine()
    {
        levelStarted = true;

        float time = (offsetZ / speed) / (sc.CalculateLength(0) / speed);
        while (time < 1f)
        {
            Evaluate(time);

            transform.position = position + (rightVector * offsetX) + (upVector * offsetY);
            transform.forward = forwardVector;

            time += Time.deltaTime / (sc.CalculateLength(0) / speed);
            yield return null;
        }

        yield return null;

        levelStarted = false;
    }

    private void Evaluate(float time)
    {
        sc.Evaluate(time, out float3 pos, out float3 tangent, out float3 up);

        position = pos;
        forwardVector = tangent;
        upVector = up;
        rightVector = Vector3.Cross(forwardVector, upVector);

        forwardVector.Normalize();
        upVector.Normalize();
        rightVector.Normalize();
    }
}