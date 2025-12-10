using UnityEngine;

public class BackToMainMenu : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject currentPanel;

    public void GoBack()
    {
        if (panelMainMenu != null)
            panelMainMenu.SetActive(true);

        if (currentPanel != null)
            currentPanel.SetActive(false);
    }
}

