using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Splines;
using Unity.Mathematics;
using Unity.VisualScripting;

public class Unit : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    
    [SerializeField] private LayerMask layerMaskSelf;
    [SerializeField] private LayerMask layerMaskGround;
    [SerializeField] private float lerpK;
    [SerializeField] private float price;
    [SerializeField] private UnitType type;
    [SerializeField] private float maxNumber;

    private Vector3 position;
    private Vector3 forwardVector;
    private Vector3 upVector;
    private Vector3 rightVector;

    private float offsetX;
    private float offsetZ;
    private float offsetY;

    private bool levelStarted = false;

    private bool selected = false;
    private bool grabbed = false;
    private Vector3 placedPosition;
    private Vector3 initialPosition;

    public SplineContainer SC { get; set; }
    public int MinX { get; set; }
    public int MinZ { get; set; }
    public int MaxX { get; set; }
    public int MaxZ { get; set; }
    public float Health { get { return _health; } set { if (value > 0) _health = value; else _health = 0; } }
    private float _health;
    public float MaxHealth { get; private set; }
    public float Price { get { return price; } }
    public UnitType Type { get { return type; } }
    public float MaxNumber { get { return maxNumber; } }
    public Vector3 StartPosition { get; set; }

    public bool IsDead { get; set; }

    private void Awake()
    {
        Health = maxHealth;
        MaxHealth = maxHealth;

        placedPosition = Vector3Int.RoundToInt(transform.position);
    }

    private void Update()
    {
        if (Health <= 0 & !IsDead)
        {
            IsDead = true;

            StopAllCoroutines();

            gameObject.SetActive(false);
        }

        if (levelStarted)
            return;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinX, MaxX), transform.position.y, Mathf.Clamp(transform.position.z, MinZ, MaxZ));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3Int cursorPosition = Vector3Int.zero;

        if (Physics.Raycast(ray, out RaycastHit hit1, 10000f, layerMaskSelf))
        {
            if (hit1.transform == transform)
                selected = true;
            else
                selected = false;
        }
        else
            selected = false;

        if (Physics.Raycast(ray, out RaycastHit hit2, 10000f, layerMaskGround))
        {
            cursorPosition = Vector3Int.RoundToInt(new Vector3(hit2.point.x, 1f, hit2.point.z));
        }

        if (selected & Input.GetKeyDown(KeyCode.Mouse0))
        {
            initialPosition = Vector3Int.RoundToInt(new Vector3(transform.position.x, 1f, transform.position.z));
            grabbed = true;
        }
        if (grabbed & Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (Physics.CheckSphere(Vector3Int.RoundToInt(new Vector3(hit2.point.x, 0f, hit2.point.z)), 0.1f, layerMaskSelf) | cursorPosition.x > MaxX | cursorPosition.z > MaxZ | cursorPosition.x < MinX | cursorPosition.z < MinZ)
                placedPosition = initialPosition;
            else
                placedPosition = cursorPosition;

            grabbed = false;

            //AudioManager.Instance.PlaySound("Place");
        }

        if (grabbed)
            transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(hit2.point.x, MinX, MaxX), 2f, Mathf.Clamp(hit2.point.z, MinZ, MaxZ)), lerpK * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, placedPosition, lerpK * Time.deltaTime);
    }

    public void OnLevelStart()
    {
        offsetX = transform.position.z;
        offsetZ = transform.position.x;
        offsetY = transform.position.y;

        StartCoroutine(OnLevelStartCoroutine());
    }

    public void OnLevelStop()
    {
        StopAllCoroutines();

        levelStarted = false;
    }

    private IEnumerator OnLevelStartCoroutine()
    {
        levelStarted = true;

        float time = (offsetZ / speed) / (SC.CalculateLength(0) / speed);
        while (time < 1f)
        {
            Evaluate(time);

            transform.position = position + (rightVector * offsetX) + (upVector * offsetY);
            transform.forward = forwardVector;

            time += Time.deltaTime / (SC.CalculateLength(0) / speed);
            yield return null;
        }

        yield return null;

        levelStarted = false;
    }

    private void Evaluate(float time)
    {
        SC.Evaluate(time, out float3 pos, out float3 tangent, out float3 up);

        position = pos;
        forwardVector = tangent;
        upVector = up;
        rightVector = Vector3.Cross(forwardVector, upVector);

        forwardVector.Normalize();
        upVector.Normalize();
        rightVector.Normalize();
    }
}

public enum UnitType
{
    Pistol,
    Rifle,
    LMG,
    Sniper
}