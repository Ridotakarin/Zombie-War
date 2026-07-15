using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [Header("Main Help Container")]
    [SerializeField] private GameObject mainHelpPanel; 

    [Header("Tutorial Pages (Panels)")]
    [SerializeField] private List<GameObject> tutorialPanels = new();

    private int currentIndex = 0;

    private void Start()
    {
        if (mainHelpPanel != null) mainHelpPanel.SetActive(false);

        ResetAllPages();
    }

    

    public void ToggleHelpPanel()
    {
        if (mainHelpPanel == null) return;

        bool isActive = !mainHelpPanel.activeSelf;
        mainHelpPanel.SetActive(isActive);

        if (isActive)
        {
            currentIndex = 0;
            ShowCurrentPage();
        }
    }

    public void NextPage()
    {
        if (tutorialPanels.Count <= 1) return;

        currentIndex++;
        if (currentIndex >= tutorialPanels.Count)
        {
            currentIndex = 0;
        }

        ShowCurrentPage();
    }

    public void PreviousPage()
    {
        if (tutorialPanels.Count <= 1) return;

        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = tutorialPanels.Count - 1;
        }

        ShowCurrentPage();
    }

    private void ShowCurrentPage()
    {
        if (tutorialPanels.Count == 0) return;

        for (int i = 0; i < tutorialPanels.Count; i++)
        {
            if (tutorialPanels[i] != null)
            {
                tutorialPanels[i].SetActive(i == currentIndex);
            }
        }
    }

    private void ResetAllPages()
    {
        for (int i = 0; i < tutorialPanels.Count; i++)
        {
            if (tutorialPanels[i] != null)
            {
                tutorialPanels[i].SetActive(false);
            }
        }
    }
}