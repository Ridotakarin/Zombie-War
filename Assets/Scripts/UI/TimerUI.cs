using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text timerText;

    private void OnEnable() => gameManager.OnTimeElapsedChanged += UpdateTimer;
    private void OnDisable() => gameManager.OnTimeElapsedChanged -= UpdateTimer;

    private void UpdateTimer(float remaining)
    {
        int m = Mathf.FloorToInt(remaining / 60f);
        int s = Mathf.FloorToInt(remaining % 60f);
        timerText.text = $"{m:00}:{s:00}";
    }
}