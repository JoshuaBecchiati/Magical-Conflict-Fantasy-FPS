using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GolemMeleeAttack enemy))
        {
            enemy.TakeDamage();
        }
    }
}