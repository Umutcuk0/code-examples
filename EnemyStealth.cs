using UnityEngine;

public class EnemyStealth : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRadius = 5f;

    public Transform player;
    public Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>(); // Animator bileþenini al
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            // Player detected, move towards the player
            Vector3 direction = player.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
            transform.LookAt(player.position);

            // Koþma animasyonunu oynat
            animator.SetBool("isRunning", true);
        }
        else
        {
            // Idle animasyonunda kal
            animator.SetBool("isRunning", false);
        }
    }

    bool CanSeePlayer()
    {
        // Check if player is within detection radius
        if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            // Check if there are no obstacles between enemy and player
            RaycastHit hit;
            Vector3 direction = player.position - transform.position;
            if (Physics.Raycast(transform.position, direction, out hit, detectionRadius))
            {
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    // Player is within detection radius and there are no obstacles
                    return true;
                }
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        // Visualize detection radius in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
