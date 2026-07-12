using UnityEngine;

public class Enemy : PoolObject, IDamageable
{
    [Header("Enemy Data")]
    [SerializeField] protected EnemyData enemyData;

    [Header("Runtime")]
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float currentMoveSpeed;

    protected DissolveEffect dissolveEffect;
    protected bool isDead;

    protected virtual void Awake()
    {
        if (enemyData == null)
        {
            Debug.LogError($"{name} is missing EnemyData.");
            enabled = false;
            return;
        }

        dissolveEffect = GetComponent<DissolveEffect>();

        if (dissolveEffect != null)
            dissolveEffect.OnDissolveFinished += OnDissolveFinished;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        currentHealth = enemyData.maxHealth;
        currentMoveSpeed = enemyData.moveSpeed;
        isDead = false;

        dissolveEffect?.ResetDissolve();
    }

    public override void OnRelease()
    {
        base.OnRelease();
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    protected virtual void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (dissolveEffect != null)
            dissolveEffect.PlayDissolve();
        else
            OnDissolveFinished();
    }

    protected virtual void OnDissolveFinished()
    {
        PoolManager.Instance.Release(this);
    }

    protected virtual void OnDestroy()
    {
        if (dissolveEffect != null)
            dissolveEffect.OnDissolveFinished -= OnDissolveFinished;
    }

    public EnemyData Data => enemyData;
    public float CurrentHealth => currentHealth;
    public float CurrentMoveSpeed => currentMoveSpeed;
}