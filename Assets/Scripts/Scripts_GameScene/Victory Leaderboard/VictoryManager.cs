using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class VictoryManager : MonoBehaviour
{
    [Header("UI Riferimenti")]
    [SerializeField] private GameObject victoryPanel; // Pannello di vittoria
    [SerializeField] private Button continueButton;   // Pulsante Continua
    [SerializeField] private Button quitButton;       // Pulsante Esci

    [Header("Testi")]
    public GameObject currentObjects;                 // Oggetto del testo "Oggetti raccolti"
    public GameObject record;                         // Oggetto del testo "Record"
    public TextMeshProUGUI currentObjectsWriting;     // Testo "Oggetti raccolti"
    public TextMeshProUGUI recordWriting;             // Testo "Record"

    private CoinDetector coinDetector;                // Script che conta gli oggetti raccolti
    private int recordScore;                          // Record salvato nei PlayerPrefs

    private void Awake()
    {
        // All'avvio il pannello è nascosto
        victoryPanel.SetActive(false);

        // Collega i pulsanti alle funzioni
        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Start()
    {
        // Trova il player e il suo script CoinDetector
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            coinDetector = player.GetComponent<CoinDetector>();
        }

        // Recupera il record salvato nei PlayerPrefs
        recordScore = PlayerPrefs.GetInt("Record", 0);
    }

    // 🔹 Mostra la schermata di vittoria
    public void ShowVictory()
    {
        Debug.Log("Hai completato il tutorial!");

        if (coinDetector == null)
        {
            Debug.LogWarning("CoinDetector non trovato sul Player!");
            return;
        }

        // Aggiorna il record se necessario
        if (coinDetector.coinNumber > recordScore)
        {
            recordScore = coinDetector.coinNumber;
            PlayerPrefs.SetInt("Record", recordScore);
            PlayerPrefs.Save();
        }

        // Mostra i valori su schermo
        currentObjects.SetActive(true);
        record.SetActive(true);
        currentObjectsWriting.text = "Oggetti raccolti: " + coinDetector.coinNumber;
        recordWriting.text = "Record: " + recordScore;

        // Mostra il pannello e blocca il gioco
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;

        // Mostra il cursore
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // 🔸 Pulsante CONTINUE
    public void ContinueGame()
    {
        Debug.Log("Continua premuto — prossima scena non ancora implementata.");
        // In futuro potrai aggiungere:
        // SceneManager.LoadScene("NomeProssimaScena");
    }

    // 🔸 Pulsante QUIT
    public void QuitGame()
    {
        Debug.Log("Chiusura del gioco...");
        Application.Quit();
    }
}

