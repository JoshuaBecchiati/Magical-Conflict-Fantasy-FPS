using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    // Riferimento allo script del player per gestire eventuali interazioni/danni
    private CharacterActions playerStats;

    // Riferimento al GameObject del giocatore
    public GameObject Player1;

    // Zona d'attacco del nemico (attivata/disattivata durante l'attacco)
    public GameObject hitZone;

    // Variabili per calcolare la distanza dal player
    private float distance;
    public float attackDistance = 2f;

    // Animator per far partire le animazioni del nemico
    public Animator animator;

    // Controlla se il nemico sta già attaccando (così non ripete subito l’attacco)
    private bool isAttacking = false;

    // Oggetto moneta che verrà generato quando il nemico muore
    public GameObject coin;
    

    // Start is called before the first frame update
    void Start() // All'avvio cerca il personaggio con il tag "Player" e prende lo script CharacterActions
    {
        GameObject Character1 = GameObject.FindGameObjectWithTag("Player");
        if (Character1 != null )
        {
            playerStats = Character1.GetComponent<CharacterActions>();
        }
    }

    // Update is called once per frame
    void Update() // Ad ogni frame controlla la distanza dal player. Se è abbastanza vicino e non sta già attaccando, parte la coroutine di attacco.
    {
        float distance = Vector3.Distance(transform.position, Player1.transform.position);
        if (distance < attackDistance && isAttacking == false)
        {
            Debug.Log("CanAttack");
            StartCoroutine(AttackTiming());
        }
    }

    // --- ATTACCO ---
    // Coroutine che gestisce i tempi dell’attacco:
    // - parte l’animazione
    // - attiva temporaneamente l’hitZone per colpire il player
    // - poi disattiva e aspetta prima del prossimo attacco
    IEnumerator AttackTiming()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.4f);
        hitZone.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitZone.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }

    // --- COLLISIONE ---
    // Se il nemico viene colpito da un oggetto con tag "PlayerDamage":
    // - istanzia (crea) una moneta nella posizione del nemico
    // - distrugge il nemico
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDamage"))
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
