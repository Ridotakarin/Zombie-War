using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FadePanelUI winPanel;
    [SerializeField] private FadePanelUI losePanel;
    private const string MenuSceneName = "Menu";


    private void OnEnable()
    {
        gameManager.OnWin += HandleWin;
        gameManager.OnLose += HandleLose;
    }

    private void OnDisable()
    {
        gameManager.OnWin -= HandleWin;
        gameManager.OnLose -= HandleLose;
    }
    public void OnExitToMenuButton() => SceneManager.LoadScene(MenuSceneName);
    private void HandleWin() => winPanel.Show();
    private void HandleLose() => losePanel.Show();
}