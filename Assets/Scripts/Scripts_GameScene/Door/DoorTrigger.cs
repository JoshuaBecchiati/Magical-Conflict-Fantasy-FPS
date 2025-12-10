using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header("Animator della porta")]
    public Animator doorAnimator;         // assegna l’Animator della porta nel Inspector

    [Header("Trigger names (devono coincidere con quelli nell’Animator)")]
    public string openTrigger = "Open";   // nome del trigger per aprire la porta
    public string closeTrigger = "Close"; // nome del trigger per chiuderla

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger(openTrigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger(closeTrigger);
        }
    }
}

