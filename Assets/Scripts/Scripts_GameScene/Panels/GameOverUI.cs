using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartButton;

    public GameObject currentObjects;
    public GameObject record;

    public TextMeshProUGUI currentObjectsWriting;
    public TextMeshProUGUI recordWriting;

    private CoinDetector coinDetector;

    private int recordScore;

    private void Awake()
    {
        gameOverPanel.SetActive(false); // all’avvio è nascosto
        restartButton.onClick.AddListener(RestartGame);
    }

    public void Start()
    {
        GameObject Player1 = GameObject.FindGameObjectWithTag("Player");
        if (Player1 != null )
        {
            coinDetector = Player1.GetComponent<CoinDetector>();
        }

        recordScore = PlayerPrefs.GetInt("Record", 0);
    }

    public void ShowGameOver()
    {
        Debug.Log("Sei morto");

        if (coinDetector.coinNumber > recordScore)
        {
            recordScore = coinDetector.coinNumber;
            PlayerPrefs.SetInt("Record", recordScore);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Next Time");
        }

        gameOverPanel.SetActive(true);

        currentObjects.SetActive(true);
        record.SetActive(true);
        currentObjectsWriting.text = "Oggetti Raccolti:" + coinDetector.coinNumber;
        recordWriting.text = "Record: " + recordScore;

        // blocca il gioco
        Time.timeScale = 0f;

        // mostra il cursore
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // ripristina tempo
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
