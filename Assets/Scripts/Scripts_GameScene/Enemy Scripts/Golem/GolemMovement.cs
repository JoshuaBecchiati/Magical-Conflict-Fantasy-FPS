using UnityEngine;
using UnityEngine.AI;
using static TutorialManager;

public class GolemMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject playerDestination;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TutorialState.tutorialCompleted)
            return; // Blocca il movimento del Golem finché il tutorial non è completato

        if (playerDestination != null)
        {
            agent.SetDestination(playerDestination.transform.position);

            // Calcola la velocità corrente del Golem
            float speed = agent.velocity.magnitude;

            // Aggiorna il parametro "isWalking" nell'animator
            if (animator != null)
            {
                animator.SetBool("isWalking", speed > 0.1f);
            }
        }
    }
}

