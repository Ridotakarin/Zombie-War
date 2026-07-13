using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
public class ZombieController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    
    private Enemy enemy;
    private NavMeshAgent agent;
    private Transform target;

    private bool isAttacking;
    private bool isDead;
    private float attackTimer;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();

        enemy.OnDead += OnDead;
        enemy.OnSpawned += Init;
        agent.speed = enemy.CurrentMoveSpeed;

    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found!");
            enabled = false;
            return;
        }

        target = player.transform;
    }

    private void Update()
    {
        if (target == null)
            return;
        if (isDead) return;
        

        attackTimer -= Time.deltaTime;

        if (isAttacking)
        {
            UpdateAnimation();
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > enemy.Data.chaseRange)
        {
            Idle();
        }
        else if (distance > enemy.Data.attackRange)
        {
            Chase();
        }
        else
        {
            Attack();
        }

        UpdateAnimation();
    }

    #region State
    private void Idle()
    {
        agent.isStopped = true;
    }

    private void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    private void Attack()
    {
        if (attackTimer > 0f)
            return;

        isAttacking = true;
        agent.isStopped = true;

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        animator.SetTrigger(AttackHash);
        Debug.Log("Attack triggered");

        attackTimer = enemy.Data.attackCooldown;
        Debug.Log($"{attackTimer} Reseted");
    }
    private void OnDead()
    {
        isDead= true;

        agent.isStopped = true;
        agent.enabled = false;

        animator.Play(Random.Range(0, 1) == 0? "Dead" : "Dead_Ex");
    }
    
#endregion

    #region Animation Event
    public void DealDamage()
    {
        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > enemy.Data.attackRange)
            return;

        if (target.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(enemy.Data.attackDamage);
        }
    }
    public void AttackFinished()
    {
        isAttacking = false;
        agent.isStopped = false;
    }
    public void DeadFinished()
    {
        enemy.PlayDissolve();
    }
    #endregion
    private void UpdateAnimation()
    {
        animator.SetFloat(SpeedHash, agent.velocity.magnitude);

    }
    private void Init()
    {
        isDead = false;
        isAttacking = false; 
        attackTimer = 0f;    

        if (agent != null)
        {
            agent.enabled = true;   
            agent.isStopped = false;
            if (agent.isOnNavMesh)
            {
                agent.ResetPath();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Enemy enemy = GetComponent<Enemy>();

        if (enemy == null || enemy.Data == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemy.Data.chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemy.Data.attackRange);
    }
}