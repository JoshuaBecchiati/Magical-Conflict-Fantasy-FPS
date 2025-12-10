using UnityEngine;
using TMPro;

public class CoinDetector : MonoBehaviour
{
    // Riferimento al testo UI dove mostro quante monete/oggetti ho raccolto
    public TextMeshProUGUI money;

    // Contatore delle monete raccolte
    public int coinNumber;

    private void OnTriggerEnter(Collider other) // Funzione che si attiva quando il player entra in contatto con un oggetto con collider (trigger)
    {
        // Controllo se l'oggetto toccato ha il tag "Money"
        if (other.CompareTag("Money"))
        {
            ++coinNumber; // Aumento il contatore delle monete di 1
            money.text = "Oggetti Raccolti: " + coinNumber; // Aggiorno il testo a schermo con il numero di oggetti raccolti
            Destroy(other.gameObject);  // Distruggo l'oggetto moneta dalla scena (lo faccio sparire)
        }
    }
}
