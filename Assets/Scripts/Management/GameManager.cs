using System;
using UnityEngine;

public enum GameState { Playing, Win, Lose }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private float hardTimeLimit = 180f;

    public event Action<float> OnTimeRemainingChanged; 
    public event Action OnWin;
    public event Action OnLose;

    public GameState CurrentState { get; private set; } = GameState.Playing;

    private float timeRemaining;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        timeRemaining = hardTimeLimit;
    }

    private void OnEnable()
    {
        if (player != null) player.OnDead += HandleLose;
    }

    private void OnDisable()
    {
        if (player != null) player.OnDead -= HandleLose;
    }

    private void Update()
    {
        if (CurrentState != GameState.Playing) return;

        timeRemaining -= Time.deltaTime;

        float displayTime = Mathf.Max(0f, timeRemaining);
        OnTimeRemainingChanged?.Invoke(displayTime);

        if (timeRemaining <= 0f)
        {
            HandleLose();
        }
    }

    public void HandleWin()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Win;
        Time.timeScale = 0f;
        OnWin?.Invoke();
    }

    private void HandleLose()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Lose;
        Time.timeScale = 0f;
        OnLose?.Invoke();
    }
}