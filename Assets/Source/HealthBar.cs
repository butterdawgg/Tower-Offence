using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform bar;
    private Unit unit;
    private Enemy enemy;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    void Update()
    {
        Vector3 scale = new Vector3(unit.Health / unit.MaxHealth, 1f, 1f);
        bar.localScale = scale;
        bar.LookAt(CameraController.Instance.Camera.transform.position, CameraController.Instance.Camera.transform.up);
    }
}
