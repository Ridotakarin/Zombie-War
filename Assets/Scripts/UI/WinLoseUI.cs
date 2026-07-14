using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void OnEnable()
    {
        gameManager.OnWin += () => winPanel.SetActive(true);
        gameManager.OnLose += () => losePanel.SetActive(true);
    }
}