using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireDamage : MonoBehaviour
{
    [Header("Danno")]
    public int damageAmount = 5;          // danno inflitto per tick
    public float damageInterval = 1f;     // ogni quanti secondi infligge danno

    [Header("Effetti")]
    public AudioSource audioSource;
    public AudioClip burnClip;             // suono quando il player entra nel falò

    private bool playerInside = false;
    private CharacterActions player;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<CharacterActions>();
            if (player != null)
            {
                playerInside = true;

                // 🔊 Suono di bruciatura
                if (audioSource != null && burnClip != null)
                    audioSource.PlayOneShot(burnClip);

                // Inizia a infliggere danni periodici
                damageCoroutine = StartCoroutine(DamageOverTime());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (playerInside && player != null)
        {
            player.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
