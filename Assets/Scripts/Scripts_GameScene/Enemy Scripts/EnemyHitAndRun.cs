using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitAndRun : MonoBehaviour
{
    // Riferimento allo script del player
    private CharacterActions playerStats;

    // Riferimento al giocatore
    public GameObject Player1;

    // Zona che diventa attiva durante l’attacco
    public GameObject hitZone;

    // Per calcolare la distanza dal player
    private float distance;

    public float attackDistance = 2f; // distanza minima per iniziare l’attacco
    public float runDistance = 5f; // distanza a cui scappa dopo l'attacco
    public float moveSpeed = 3f;   // velocità del nemico

    // Gestione delle animazioni
    public Animator animator;

    // Controllo stato del nemico (attacco o fuga)
    private bool isAttacking = false;
    private bool isRunning = false;

    // Oggetto moneta che rilascia quando muore
    public GameObject coin;

    void Start() // All’avvio cerca il player tramite il tag e prende lo script CharacterActions
    {
        GameObject Character1 = GameObject.FindGameObjectWithTag("Player");
        if (Character1 != null)
        {
            playerStats = Character1.GetComponent<CharacterActions>();
        }
    }

    // Ad ogni frame calcola la distanza dal player
    // - Se non sta attaccando né scappando, si avvicina al giocatore
    // - Quando è abbastanza vicino, parte la coroutine AttackAndRun
    void Update()
    {
        distance = Vector3.Distance(transform.position, Player1.transform.position);

        // Se è lontano si muove verso il player
        if (!isAttacking && !isRunning)
        {
            // avvicinati al giocatore
            if (distance > attackDistance)
            {
                Vector3 direction = (Player1.transform.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
            // Se è vicino abbastanza, attacca
            else
            {
                StartCoroutine(AttackAndRun());
            }
        }
    }

    // --- ATTACCO E FUGA ---
    // Coroutine che gestisce il comportamento “hit and run”:
    // 1. Attacca il giocatore
    // 2. Si allontana correndo più velocemente
    IEnumerator AttackAndRun()
    {
        isAttacking = true;

        // Attacco
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.4f);
        hitZone.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitZone.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        isAttacking = false;

        // Run away
        isRunning = true;
        Vector3 runDirection = (transform.position - Player1.transform.position).normalized; // allontanati dal player
        float runTime = 1f; // durata del run
        float elapsed = 0f;

        // Finché non passa il tempo stabilito, il nemico corre via dal player
        while (elapsed < runTime)
        {
            transform.position += runDirection * moveSpeed * 2f * Time.deltaTime; // più veloce del normale
            elapsed += Time.deltaTime;
            yield return null;
        }

        isRunning = false;
    }

    // --- COLLISIONE ---
    // Se il nemico viene colpito da un oggetto con tag "PlayerDamage":
    // - genera una moneta
    // - si distrugge
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDamage"))
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

