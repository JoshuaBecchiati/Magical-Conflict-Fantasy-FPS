using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject panelStartScreen;
    public GameObject panelMainMenu;

    private bool hasStarted = false;

    void Start()
    {
        // All’avvio mostriamo solo lo start screen
        panelStartScreen.SetActive(true);
        panelMainMenu.SetActive(false);

        // Mostra e sblocca il cursore
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0)) // tasto o click sinistro
            {
                hasStarted = true;

                panelStartScreen.SetActive(false);
                panelMainMenu.SetActive(true);
            }
        }
    }
}
