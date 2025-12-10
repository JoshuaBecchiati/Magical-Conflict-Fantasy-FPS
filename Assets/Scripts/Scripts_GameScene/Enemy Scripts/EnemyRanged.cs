using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    private CharacterActions playerStats;
    public GameObject Player1;
    public GameObject projectilePrefab; // Prefab della sfera da lanciare
    public Transform shootPoint; // Punto da cui parte il proiettile
    public float attackDistance = 10f;
    public float projectileSpeed = 10f;
    public Animator animator;
    private bool isAttacking = false;

    public GameObject coin;

    void Start()
    {
        GameObject Character1 = GameObject.FindGameObjectWithTag("Player");
        if (Character1 != null)
        {
            playerStats = Character1.GetComponent<CharacterActions>();
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player1.transform.position);
        if (distance < attackDistance && !isAttacking)
        {
            StartCoroutine(AttackTiming());
        }
    }

    IEnumerator AttackTiming()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.4f); // Tempo per anticipare l'attacco

        // Istanzia il proiettile
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (Player1.transform.position - shootPoint.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }

        yield return new WaitForSeconds(1.5f); // Cooldown tra un attacco e l'altro
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDamage"))
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

