using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class Unit : MonoBehaviour
{
    [SerializeField] private SplineContainer sc;

    [SerializeField] [Range(0f, 1f)] private float _time;

    [SerializeField] private float offset;

    private Vector3 position;
    private Vector3 tangent;
    private Vector3 upVector;


    private void Awake()
    {
        Evaluate(0f);
    }

    private void Update()
    {
        Evaluate(_time);

        transform.position = position + (Vector3.Cross(tangent, upVector).normalized * offset);
    }

    private void Evaluate(float time)
    {
        sc.Evaluate(time, out float3 positionTemp, out float3 tangentTemp, out float3 upVectorTemp);

        position = positionTemp;
        tangent = tangentTemp;
        upVector = upVectorTemp;
    }
}