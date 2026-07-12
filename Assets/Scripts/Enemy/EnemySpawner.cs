using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawn Setting")]
    [SerializeField] private string enemyID = "Zombie";
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private bool autoSpawn = true;
    [SerializeField] private List<Transform> spawnPos;
    
    private float spawnTimer;


    void Update()
    {
        if(!autoSpawn) return;
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }
    }

    [ContextMenu("Spawn Enemy")]
    public void SpawnEnemy()
    {
        PoolObject obj = PoolManager.Instance.Spawn(enemyID);
        if (obj == null) return;
        int randomIndex = UnityEngine.Random.Range(0, spawnPos.Count);
        obj.transform.SetPositionAndRotation(spawnPos[randomIndex].position, Quaternion.identity);
    }
}
