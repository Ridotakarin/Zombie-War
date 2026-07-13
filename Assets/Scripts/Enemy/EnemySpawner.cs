using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Setting")]
    [SerializeField] private string enemyID = "Zombie";
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private float spawnRadius = 1.5f;

    [ContextMenu("Spawn 1 Enemy")]
    public void SpawnEnemy()
    {
        SpawnEnemies(1);
    }

    public void SpawnEnemies(int amount)
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning($"{name}: No Spawn Points.");
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            PoolObject obj = PoolManager.Instance.Spawn(enemyID);

            if (obj == null)
                return;

            Transform point = spawnPoints[Random.Range(0, spawnPoints.Count)];

            Vector2 offset = Random.insideUnitCircle * spawnRadius;

            Vector3 spawnPosition = point.position;
            spawnPosition.x += offset.x;
            spawnPosition.z += offset.y;

            obj.transform.SetPositionAndRotation(spawnPosition, point.rotation);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        foreach (Transform point in spawnPoints)
        {
            if (point == null) continue;

            Gizmos.DrawWireSphere(point.position, spawnRadius);
        }
    }
#endif
}