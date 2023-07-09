using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Transform bar;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        Vector3 scale = new Vector3(enemy.Health / enemy.MaxHealth, 1f, 1f);
        bar.localScale = scale;
        bar.LookAt(CameraController.Instance.Camera.transform.position, CameraController.Instance.Camera.transform.up);
    }
}
