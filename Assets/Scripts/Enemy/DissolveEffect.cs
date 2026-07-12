using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class DissolveEffect: MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;

    private MaterialPropertyBlock block;
    private static readonly int DissolveID = Shader.PropertyToID("_Dissolve");

    public event Action OnDissolveFinished;
    private void Awake()
    {
        block = new MaterialPropertyBlock();
    }

    public void PlayDissolve()
    {
        StartCoroutine(DissolveRoutine());
    }
    public void ResetDissolve()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.GetPropertyBlock(block);

            block.SetFloat(DissolveID, 0f);

            renderer.SetPropertyBlock(block);
        }
    }

    public IEnumerator DissolveRoutine()
    {
        float value = 0f;

        while(value < 1f)
        {
            value += Time.deltaTime;
            foreach (Renderer r in renderers)
            {
                r.GetPropertyBlock(block);
                block.SetFloat(DissolveID, value);
                r.SetPropertyBlock(block);
            }
            yield return null;
        }
        OnDissolveFinished?.Invoke();
    }
    
}
