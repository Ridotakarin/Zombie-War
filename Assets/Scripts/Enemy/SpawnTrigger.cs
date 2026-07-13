using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private bool triggerOnce = true;

    private bool activated;

    private void Reset()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;

        if (enemySpawner == null)
            enemySpawner = GetComponentInChildren<EnemySpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (triggerOnce && activated)
            return;

        activated = true;
        Debug.Log("Player trigger spawner");

        enemySpawner.SpawnEnemies(enemyCount);
    }
}