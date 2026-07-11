using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string id;
    public PoolObject prefab;
    public int size = 10;
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField]
    private List<Pool> pools = new();

    private readonly Dictionary<string, Queue<PoolObject>> poolDictionary = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        foreach (Pool pool in pools)
        {
            Queue<PoolObject> queue = new();

            for (int i = 0; i < pool.size; i++)
            {
                PoolObject obj = Instantiate(pool.prefab, transform);

                obj.poolID = pool.id;
                obj.OnRelease();

                queue.Enqueue(obj);
            }

            poolDictionary.Add(pool.id, queue);
        }
    }

    public T Spawn<T>(string id) where T : PoolObject
    {
        if (!poolDictionary.TryGetValue(id, out Queue<PoolObject> queue))
        {
            Debug.LogError($"Pool [{id}] không tồn tại.");
            return null;
        }

        if (queue.Count == 0)
        {
            Debug.LogWarning($"Pool [{id}] đã hết object.");
            return null;
        }

        PoolObject obj = queue.Dequeue();
        obj.OnSpawn();

        return obj as T;
    }

    public void Release(PoolObject obj)
    {
        if (obj == null)
            return;

        if (!poolDictionary.TryGetValue(obj.poolID, out Queue<PoolObject> queue))
        {
            Debug.LogError($"Pool [{obj.poolID}] không tồn tại.");
            return;
        }

        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        obj.OnRelease();

        queue.Enqueue(obj);
    }
}