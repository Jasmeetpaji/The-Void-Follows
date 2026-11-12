using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;

    [Header("Movement")]
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 5f;
    public float detectionRange = 15f;

    [Header("Detection")]
    public float fieldOfView = 100f;
    public LayerMask obstacleMask;

    [Header("Search")]
    public float memoryDuration = 5f;

    private NavMeshAgent agent;
    private int currentPatrolPoint = 0;

    private bool chasing = false;
    private Vector3 lastSeenPosition;
    private float lastSeenTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = patrolSpeed;

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[0].position);
        }
    }

    void Update()
    {
        if (player == null || !agent.isOnNavMesh)
        return;

        if (CanSeePlayer())
        {
            chasing = true;
            lastSeenPosition = player.position;
            lastSeenTime = Time.time;

            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
        }
        else if (chasing && Time.time - lastSeenTime < memoryDuration)
        {
            agent.SetDestination(lastSeenPosition);
        }
        else
        {
            chasing = false;
            Patrol();
        }
    }

    bool CanSeePlayer()
    {
        Vector3 direction = player.position - transform.position;

        if (direction.magnitude > detectionRange)
        return false;

        float angle = Vector3.Angle(
            transform.forward,
            direction
        );

        if (angle > fieldOfView / 2f)
        return false;

        Vector3 eyePosition = transform.position + Vector3.up * 1.5f;

        Vector3 playerPosition = player.position + Vector3.up;

        if (Physics.Linecast(
            eyePosition,
            playerPosition,
            obstacleMask))
        {
            return false;
        }

        return true;
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        return;

        agent.speed = patrolSpeed;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolPoint++;

            if (currentPatrolPoint >= patrolPoints.Length)
            currentPatrolPoint = 0;

            agent.SetDestination(
                patrolPoints[currentPatrolPoint].position
            );
        }
    }
}