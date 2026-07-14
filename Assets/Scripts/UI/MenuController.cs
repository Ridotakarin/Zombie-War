using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //[SerializeField] private string gameplaySceneName = "Gameplay";
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject stageSelectPanel;

    private void Start()
    {
        AudioManager.Instance?.PlayMusic(menuMusic);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        if (stageSelectPanel != null)
            stageSelectPanel.SetActive(false);
    }

    public void OnPlayButton() => stageSelectPanel.SetActive(true);
    public void OnStage1Button() => SceneManager.LoadScene("Gameplay_Map1");
    public void OnQuitButton() => Application.Quit();
    public void OpenSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }
}