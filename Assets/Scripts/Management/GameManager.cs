using System;
using UnityEngine;
using UnityEngine.Rendering;

public enum GameState { Playing, Win, Lose }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;

    public event Action<float> OnTimeElapsedChanged; // chỉ để hiển thị HUD
    public event Action OnWin;
    public event Action OnLose;

    public GameState CurrentState { get; private set; } = GameState.Playing;
    private float timeElapsed;
    private float hardLimit = 300f;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void OnEnable() => player.OnDead += HandleLose;
    private void OnDisable() => player.OnDead -= HandleLose;

    private void Update()
    {
        if (CurrentState != GameState.Playing)
            return;

        timeElapsed += Time.deltaTime;
        if(timeElapsed >= hardLimit)
            HandleLose();
        OnTimeElapsedChanged?.Invoke(timeElapsed);
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