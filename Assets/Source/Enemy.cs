using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    public float Health { get { return _health; } set { if (value > 0) _health = value; else _health = 0; } }
    private float _health;

    private void Awake()
    {
        Health = maxHealth;
    }

    private void Update()
    {
        if (Health <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
