using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadePanelUI : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.6f;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        canvasGroup.alpha = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}