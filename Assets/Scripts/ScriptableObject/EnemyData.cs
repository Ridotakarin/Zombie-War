using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Name")]
    public string enemyName = "Zombie";

    [Header("Stats")]
    [Min(1f)]
    public float maxHealth = 100f;

    [Min(0f)]
    public float moveSpeed = 3f;

    [Min(0f)]
    public float attackRange = 1.5f;

    [Min(0f)]
    public float attackDamage = 10f;

    [Min(0f)]
    public float attackCooldown = 1f;

    [Header("Detection")]
    [Min(0f)]
    public float chaseRange = 15f;

    [Header("Animation")]
    public float attackAnimationDelay = 0.3f;


    [Header("Effects")]
    public ParticleSystem hitEffect;
    public ParticleSystem deathEffect;
}
