using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] private PlayerData playerData;

    [Header("Runtime")]
    [SerializeField] private float currentHealth;

    private bool isDead;

    public event Action OnDead;

    private void Awake()
    {
        if (playerData == null)
        {
            Debug.LogError($"{name} is missing PlayerData.");
            enabled = false;
            return;
        }

        Initialize();
    }

    private void Initialize()
    {
        currentHealth = playerData.maxHealth;
        isDead = false;
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        Debug.Log($"Player HP : {currentHealth}");

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("Player Dead");

        OnDead?.Invoke();
    }

    public bool IsDead => isDead;
    public float CurrentHealth => currentHealth;
}