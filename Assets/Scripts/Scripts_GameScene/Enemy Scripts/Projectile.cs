using UnityEngine;

public class Projectile : MonoBehaviour
{
    private CharacterActions playerStats;

    public float lifetime = 5f; // Durata prima di auto-distruggersi
    public int damage = 10;

    void Start()
    {
        GameObject Character1 = GameObject.FindGameObjectWithTag("Player");
        if (Character1 != null)
        {
            playerStats = Character1.GetComponent<CharacterActions>();
        }

        Destroy(gameObject, lifetime); // Distrugge il proiettile dopo un po'
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Applica danno al player
            CharacterActions player = other.GetComponent<CharacterActions>();
            if (player != null && playerStats.isDefending == false)
            {
                player.TakeDamage(damage); // Assicurati che CharacterActions abbia TakeDamage()
            }

            Destroy(gameObject); // Distrugge il proiettile al contatto
        }

        // Se vuoi farlo distruggere anche su muri o oggetti
        if (other.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}

