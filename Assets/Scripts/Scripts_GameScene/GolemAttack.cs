using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    [SerializeField] private float m_damage = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterAction player))
        {
            player.TakeDamage(m_damage);
        }
    }
}
