using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterAction : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float m_health = 100f;

    [Header("Weapon hitbox")]
    [SerializeField] private Collider m_hitBoxAttack;

    [Header("Audio")]
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_attackClip;
    [SerializeField] private AudioClip m_damageClip;
    [SerializeField] private AudioClip m_deathClip;

    [Header("Animator")]
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_attack1 = "Attack1";
    [SerializeField] private string m_attack2 = "Attack2";
    [SerializeField] private string m_attack3 = "Attack3";

    [Header("Character movements")]
    [SerializeField] private CharacterController m_characterController;
    [SerializeField] private CharacterMovement m_characterMovement;

    [Header("Events")]
    public UnityEvent onDeath;

    private Coroutine _comboCoroutine;
    private int _attackIndex = 1;
    private bool _isBusy;
    private bool _isAttacking;
    private bool _isDefending;
    private bool _isInvincible;

    public float Health => m_health;

    private void OnValidate()
    {
        if (!m_animator) m_animator = GetComponent<Animator>();
        if (!m_characterController) m_characterController = GetComponent<CharacterController>();
        if (!m_characterMovement) m_characterMovement = GetComponent<CharacterMovement>();
    }

    private void Start()
    {
        if (PlayerInputSingleton.instance != null)
        {
            PlayerInputSingleton.instance.Actions["Attack"].performed += OnAttackInput;
            PlayerInputSingleton.instance.Actions["Defend"].started += OnDefendStart;
            PlayerInputSingleton.instance.Actions["Defend"].canceled += OnDefendEnd;
        }
    }
    private void OnDestroy()
    {
        if (PlayerInputSingleton.instance != null)
        {
            PlayerInputSingleton.instance.Actions["Attack"].performed -= OnAttackInput;
            PlayerInputSingleton.instance.Actions["Defend"].started -= OnDefendStart;
            PlayerInputSingleton.instance.Actions["Defend"].canceled -= OnDefendEnd;
        }
    }

    public void TakeDamage(float amount)
    {
        if (_isInvincible) return;

        if (_isDefending)
            m_health -= amount / 2;
        else
            m_health -= amount;

        if (m_damageClip != null && m_audioSource != null)
            m_audioSource.PlayOneShot(m_damageClip);

        m_animator.CrossFade("Hit", 0.05f, 0, 0);

        if (m_health <= 0)
            Death();

        StartCoroutine(ResetInvincibility());
    }

    private IEnumerator ResetInvincibility()
    {
        _isInvincible = true;

        yield return new WaitForSeconds(1f);

        _isInvincible = false;
    }

    void Death()
    {
        m_animator.CrossFade("Death", 0.05f, 0, 0);

        if (m_deathClip != null && m_audioSource != null)
            m_audioSource.PlayOneShot(m_deathClip);

        StartCoroutine(HandleDeathAfterDelay());
    }

    IEnumerator HandleDeathAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        onDeath?.Invoke();
    }

    public void HandleDeath()
    {
        Destroy(gameObject);
        Time.timeScale = 0f;
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        if (_isAttacking || _isBusy) return;

        m_animator.SetBool("IsAttacking", true);
        Attack(_attackIndex);
    }

    private void OnDefendStart(InputAction.CallbackContext context)
    {
        if ((_isAttacking || _isBusy) && _isDefending) return;

        m_animator.SetBool("IsDefending", true);

        _isDefending = true;
        m_characterMovement.SetDefending(true);
    }

    private void OnDefendEnd(InputAction.CallbackContext context)
    {
        if ((_isAttacking || _isBusy) && !_isDefending) return;

        m_animator.SetBool("IsDefending", false);

        _isDefending = false;
        m_characterMovement.SetDefending(false);
    }

    private void Attack(int attack)
    {
        switch (attack)
        {
            case 1:
                {
                    RestartCoroutine();
                    m_animator.CrossFade(m_attack1, .05f, 0, 0);
                }
                break;
            case 2:
                {
                    RestartCoroutine();
                    m_animator.CrossFade(m_attack2, .05f, 0, 0);
                }
                break;
            case 3:
                {
                    RestartCoroutine();
                    m_animator.CrossFade(m_attack3, .05f, 0, 0);
                }
                break;
        }
    }

    private void RestartCoroutine()
    {
        if (_comboCoroutine != null)
            StopCoroutine(_comboCoroutine);

        _comboCoroutine = StartCoroutine(ComboReset());
    }

    private IEnumerator ComboReset()
    {
        _isBusy = true;
        _isAttacking = true;
        yield return new WaitForSeconds(.5f);

        if (_attackIndex <= 4)
            _attackIndex++;
        _isBusy = false;
        _isAttacking = false;

        yield return new WaitForSeconds(.25f);

        m_animator.SetBool("IsAttacking", false);
        _attackIndex = 1;
    }

    public void AttackBegin()
    {
        m_audioSource.PlayOneShot(m_attackClip);
        m_hitBoxAttack.enabled = true;
    }

    public void AttackStop()
    {
        m_hitBoxAttack.enabled = false;
    }
}
