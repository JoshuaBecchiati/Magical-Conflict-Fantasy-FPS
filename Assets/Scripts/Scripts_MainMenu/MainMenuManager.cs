using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject panelOptions;
    public GameObject panelCredits;
    public GameObject panelNotices;
    public GameObject panelStartScreen;

    public GameObject panelControls;

    // Mostra il menu principale
    public void ShowMainMenu()
    {
        panelMainMenu.SetActive(true);
        panelOptions.SetActive(false);
        panelCredits.SetActive(false);
        panelNotices.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Backup Foresta HDRP");
    }

    public void ShowOptions()
    {
        panelMainMenu.SetActive(false);
        panelOptions.SetActive(true);
    }

    public void ShowCredits()
    {
        panelMainMenu.SetActive(false);
        panelCredits.SetActive(true);
    }

    public void ShowNotices()
    {
        panelMainMenu.SetActive(false);
        panelNotices.SetActive(true);
    }

    public void ShowControls()
    {
        panelMainMenu.SetActive(false);
        panelControls.SetActive(true);
    }
    public void QuitGame()
    {
        // Mostra StartScreen
        panelStartScreen.SetActive(true);

        // Nasconde tutti gli altri pannelli
        panelMainMenu.SetActive(false);
        panelOptions.SetActive(false);
        panelCredits.SetActive(false);
        panelNotices.SetActive(false);
        panelControls.SetActive(false);
    }
}

