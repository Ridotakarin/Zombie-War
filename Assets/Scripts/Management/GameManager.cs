using System;
using UnityEngine;

public enum GameState { Playing, Win, Lose }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private float hardTimeLimit = 180f;

    [Header("Audio")]
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    public event Action<float> OnTimeRemainingChanged;
    public event Action OnWin;
    public event Action OnLose;

    public GameState CurrentState { get; private set; } = GameState.Playing;
    public bool IsPlaying => CurrentState == GameState.Playing;   // NEW — thay cho việc dựa vào timeScale

    private float timeRemaining;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        timeRemaining = hardTimeLimit;
    }
    private void Start()
    {
        AudioManager.Instance?.PlayMusic(gameplayMusic);
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
            HandleLose();
    }

    public void HandleWin()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Win;
        AudioManager.Instance?.PlaySFX(winSound);
        OnWin?.Invoke();
    }

    private void HandleLose()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Lose;
        AudioManager.Instance?.PlaySFX(loseSound);
        OnLose?.Invoke();
    }
}