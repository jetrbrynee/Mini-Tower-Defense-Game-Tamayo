using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public Transform path;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private Rigidbody2D rb;
    private void Start()
    {
        waypoints = new Transform[path.childCount];
        for (int i = 0; i < path.childCount; i++)
        {
            waypoints[i] = path.GetChild(i);
        }
    }

    private void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector2 targetPosition = waypoints[currentWaypointIndex].position;
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

            if (rb != null)
            {
                rb.velocity = moveDirection * moveSpeed;
            }

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}
