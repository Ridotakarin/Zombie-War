using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private const string MenuSceneName = "Menu";

    private bool isPaused;

    public void TogglePause()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying)
            return; 

        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void OnContinueButton() => TogglePause();

    public void OnExitToMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MenuSceneName);
    }
}