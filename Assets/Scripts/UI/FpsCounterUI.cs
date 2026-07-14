using TMPro;
using UnityEngine;

public class FpsCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text fpsText;
    [SerializeField] private float updateInterval = 0.5f;

    private float accumTime;
    private int frameCount;

    private void Update()
    {
        // unscaledDeltaTime vì lúc Win/Lose Time.timeScale = 0, không nên khiến FPS counter đứng hình
        accumTime += Time.unscaledDeltaTime;
        frameCount++;

        if (accumTime >= updateInterval)
        {
            float fps = frameCount / accumTime;
            fpsText.text = $"{fps:0} FPS";
            accumTime = 0f;
            frameCount = 0;
        }
    }
}