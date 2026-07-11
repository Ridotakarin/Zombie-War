using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [HideInInspector]
    public string poolID;

    public virtual void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnRelease()
    {
        gameObject.SetActive(false);
    }
}