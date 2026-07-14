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
    public event Action OnTakeDamage;
    public event Action<float, float> OnHealthChanged;

    public float MaxHealth => playerData.maxHealth;

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
        OnHealthChanged?.Invoke(currentHealth, playerData.maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        OnTakeDamage?.Invoke();

        Debug.Log($"Player HP : {currentHealth}");

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
        OnHealthChanged?.Invoke(currentHealth, playerData.maxHealth);
    }

    private void Die()
    {
        if (isDead)
            return;


        isDead = true;

        Debug.Log("Player Dead");

        OnDead?.Invoke();
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth + amount, playerData.maxHealth); 
        OnHealthChanged?.Invoke(currentHealth, playerData.maxHealth);
    }    

    public bool IsDead => isDead;
    public float CurrentHealth => currentHealth;
}