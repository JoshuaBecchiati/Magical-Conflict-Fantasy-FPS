using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // evita che si attivi più volte

            // Trova lo script VictoryUI nella scena e chiama la funzione
            VictoryManager victoryUI = FindObjectOfType<VictoryManager>();

            if (victoryUI != null)
            {
                victoryUI.ShowVictory();
            }
            else
            {
                Debug.LogWarning("VictoryUI non trovato nella scena!");
            }
        }
    }
}

