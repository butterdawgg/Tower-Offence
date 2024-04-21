using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float moneyDropAmount;

    public float Health { get { return _health; } set { if (value > 0) _health = value; else _health = 0; } }
    private float _health;
    public float MaxHealth { get; private set; }
    public bool IsDead { get; set; }

    private void Awake()
    {
        Health = maxHealth;
        MaxHealth = maxHealth;
    }

    private void Update()
    {
        if (Health <= 0 & !IsDead)
        {
            SerializeManager.SetFloat(FloatType.Money, SerializeManager.GetFloat(FloatType.Money) + moneyDropAmount);

            IsDead = true;

            gameObject.SetActive(false);
        }
    }
}
