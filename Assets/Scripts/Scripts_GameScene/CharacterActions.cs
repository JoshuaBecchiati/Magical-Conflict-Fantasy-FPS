using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterActions : MonoBehaviour
{
    public float attackTimer = 4f;
    public Animator animator;
    public bool isDefending = false;
    public bool isAttacking = false;
    public CharacterMovement characterMovement;
    public bool noMovement = false;

    public float health;
    public bool dead;

    public GameObject axeZone;

    public AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip damageClip; // <-- nuovo: suono quando subisce danno
    public AudioClip deathClip;  // <-- nuovo: suono quando muore

    [Header("Events")]
    public UnityEvent onDeath; // <-- evento richiamato quando il player muore

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ! isDefending)
        {
            DefendMode();
        }
        if (Input.GetKeyUp(KeyCode.Space) && isDefending)
        {
            DefendModeOff();
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
        {
            StartCoroutine(AttackMode());
        }

        if (health<=0 && dead == false)
        {
           Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage") && !isDefending)
        {
            //health--;

            // riduzione vita + suono
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        if (dead) return; // Se è già morto non subisce più danni

        health -= amount;

        // Effetto audio
        if (damageClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageClip);
        }

        // Eventuale animazione di danno
        animator.SetTrigger("Hit");

        if (health <= 0)
        {
            Death();
        }
    }


    void DefendMode()
    {
        isDefending = true;
        animator.SetBool("Defend", true);
        noMovement = true;
    }

    void DefendModeOff()
    {
        animator.SetBool("Defend", false);
        isDefending=false;
        noMovement = false;
    }

    IEnumerator AttackMode()
    {
        isAttacking = true;
        noMovement = true;
        animator.SetBool("Attack", true);
        axeZone.SetActive(true);

        audioSource.PlayOneShot(attackClip);

        yield return new WaitForSeconds(attackTimer);
        axeZone.SetActive(false);
        animator.SetBool("Attack", false);
        isAttacking = false;
        noMovement = false;
    }

    void Death()
    {
        dead = true;
        noMovement = true;
        animator.SetTrigger("Die");

        // Effetto audio di morte
        if (deathClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathClip);
        }

        // Aspetta un paio di secondi prima di mostrare la leaderboard
        StartCoroutine(HandleDeathAfterDelay());
    }
    //Notifica la UI(GameOver)
    //onDeath?.Invoke();

    IEnumerator HandleDeathAfterDelay()
    {
        yield return new WaitForSeconds(3f); // cambia con la durata dell'animazione
        onDeath?.Invoke();
    }
}
