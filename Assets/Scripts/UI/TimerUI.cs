using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text timerText;

    private Dictionary<int, string> timeStringCache;
    private int lastSecond = -1;
    private const int MAX_CACHE_SECONDS = 200;

    private void Awake()
    {
        PrecomputeTimeStrings();
    }
    private void OnEnable()
    {
        if (gameManager != null) gameManager.OnTimeRemainingChanged += UpdateTimer;
    }
    private void OnDisable()
    {
        if (gameManager != null) gameManager.OnTimeRemainingChanged -= UpdateTimer;
    }
    private void PrecomputeTimeStrings()
    {
        timeStringCache = new Dictionary<int, string>();
        for (int i = 0; i <= MAX_CACHE_SECONDS; i++)
        {
            timeStringCache[i] = FormatTime(i);
        }
    }
    private void UpdateTimer(float remainingTime)
    {
        int totalSeconds = Mathf.CeilToInt(remainingTime);
        if (totalSeconds == lastSecond) return;
        lastSecond = totalSeconds;

        if (timeStringCache.TryGetValue(totalSeconds, out string timeStr))
        {
            timerText.text = timeStr;
        }
        else
        {
            timerText.text = FormatTime(totalSeconds);
        }
    }
    private string FormatTime(int totalSeconds)
    {
        int m = totalSeconds / 60;
        int s = totalSeconds % 60;
        return $"{m:00}:{s:00}";
    }
}