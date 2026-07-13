using System;
using System.Collections;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Renderer[] renderers;

    [Header("Settings")]
    [Tooltip("Thời gian tan biến hoàn toàn tính bằng giây")]
    [SerializeField] private float dissolveDuration = 1.5f;

    private MaterialPropertyBlock block;
    private static readonly int DissolveID = Shader.PropertyToID("_Dissolve");

    public event Action OnDissolveFinished;

    private void Awake()
    {
        block = new MaterialPropertyBlock();
    }

    public void PlayDissolve()
    {
        StopAllCoroutines();
        StartCoroutine(DissolveRoutine());
    }

    public void ResetDissolve()
    {
        UpdateRenderersProperty(0f);
    }

    private IEnumerator DissolveRoutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsedTime / dissolveDuration);

            UpdateRenderersProperty(progress);
            yield return null;
        }
        UpdateRenderersProperty(1f);

        OnDissolveFinished?.Invoke();
    }
    private void UpdateRenderersProperty(float value)
    {
        block.Clear();
        block.SetFloat(DissolveID, value);

        foreach (Renderer renderer in renderers)
        {
            if (renderer == null)
                continue;

            renderer.SetPropertyBlock(block);
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}