using System;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    [Header("Enemy Data")]
    [SerializeField] private EnemyData enemyData;

    [Header("Run Time Variables")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float currentMoveSpeed;

    private void Awake()
    {
        if(enemyData == null)
        {
            Debug.LogError("EnemyData is not assigned in the inspector.");
            return;
        }

        Initialize();
    }

    private void Initialize()
    {
        currentHealth = enemyData.maxHealth;
        currentMoveSpeed = enemyData.moveSpeed;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // Implement damage logic here
        Debug.Log($"{name} took {damage} damage.");

        if( currentHealth < 0 )
        {
            currentHealth = 0;
            Die();
        }
    }
    protected virtual void Die()
    {
        // Implement death logic here
        Debug.Log($"{name} has died.");
        Destroy(gameObject,1f);
    }

    public float CurrentMoveSpeed => currentMoveSpeed;
    public void SetHealth(float health)
    {
        currentHealth = health;
    }
    public void SetMoveSpeed(float moveSpeed)
    {
        currentMoveSpeed = moveSpeed;
    }

}
