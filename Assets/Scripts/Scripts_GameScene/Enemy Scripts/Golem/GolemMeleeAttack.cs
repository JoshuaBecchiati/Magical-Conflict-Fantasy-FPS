using System.Collections;
using UnityEngine;

public class GolemMeleeAttack : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    [SerializeField] private Collider m_hitBox;
    [SerializeField] private Animator m_animator;
    [SerializeField] private GameObject m_coin;
    [SerializeField, Range(1f, 5f)] private float m_attackDistance = 2f;

    private bool isAttacking = false;

    private void OnValidate()
    {
        m_animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Trova il giocatore se non assegnato manualmente
        if (m_player == null)
            m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (m_player == null) return;

        float distance = Vector3.Distance(transform.position, m_player.transform.position);

        if (distance < m_attackDistance && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        if (m_animator != null)
            m_animator.SetTrigger("Attack");

        yield return new WaitForSeconds(2f);

        isAttacking = false;
    }

    public void AttackBegin()
    {
        m_hitBox.enabled = true;
    }

    public void AttackEnd()
    {
        m_hitBox.enabled = false;
    }

    public void TakeDamage()
    {
        Instantiate(m_coin, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}


