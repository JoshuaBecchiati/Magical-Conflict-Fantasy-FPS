using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [Header("UI Riferimenti")]
    // Riferimento al testo dell'obiettivo nella UI
    public TMP_Text objectiveText;

    // Indice dell'obiettivo attuale
    private int currentObjectiveIndex = 0;

    // Lista degli obiettivi
    private string[] objectives = new string[]
    {
        "Sconfiggi tutti i nemici",
        "Raggiungi la casa del Mago sulla collina"
    };

    // Per evitare di aggiornare continuamente dopo aver cambiato obiettivo
    private bool allEnemiesDefeated = false;

    void Start()
    {
        // Imposta il primo obiettivo all'avvio
        ShowCurrentObjective();
    }

    void Update()
    {
        // Se siamo al primo obiettivo (indice 0)
        if (currentObjectiveIndex == 0 && !allEnemiesDefeated)
        {
            // Conta quanti oggetti con tag "Enemy" ci sono ancora nella scena
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Se non ce n'è più nessuno...
            if (enemies.Length == 0)
            {
                allEnemiesDefeated = true;
                NextObjective(); // passa al secondo obiettivo
            }
        }
    }

    // Mostra l'obiettivo attuale
    void ShowCurrentObjective()
    {
        if (objectiveText != null && currentObjectiveIndex < objectives.Length)
        {
            objectiveText.text = "Obiettivo: " + objectives[currentObjectiveIndex];
        }
    }

    // Passa all'obiettivo successivo
    public void NextObjective()
    {
        currentObjectiveIndex++;

        if (currentObjectiveIndex < objectives.Length)
        {
            ShowCurrentObjective();
        }
        else
        {
            objectiveText.text = "Tutti gli obiettivi completati!";
        }
    }

    // Facoltativo: per resettare gli obiettivi se vuoi riavviare
    public void ResetObjectives()
    {
        currentObjectiveIndex = 0;
        allEnemiesDefeated = false;
        ShowCurrentObjective();
    }
}

