using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public Transform player;
    public float patrolRange = 10f;
    public float chaseRange = 15f;
    public float attackRange = 5f;
    public float attackRate = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private NavMeshAgent agent;
    private Vector3 patrolDestination;
    private bool isPatrolling = true; // Mulai dengan mode patroli
    private bool isChasing;
    private bool isAttacking;
    private float nextAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewPatrolDestination();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isPatrolling)
        {
            agent.destination = patrolDestination;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                SetNewPatrolDestination();
            }

            if (distanceToPlayer <= chaseRange)
            {
                isPatrolling = false;
                isChasing = true;
            }
        }
        else
        {
            if (distanceToPlayer <= chaseRange)
            {
                isChasing = true;
                agent.destination = player.position;
            }
            else
            {
                isChasing = false;
                isPatrolling = true;
                SetNewPatrolDestination();
            }
        }

        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            isAttacking = true;
            AttackPlayer();
            nextAttackTime = Time.time + 1f / attackRate;
        }
        else
        {
            isAttacking = false;
        }
    }

    void SetNewPatrolDestination()
    {
        Vector3 randomDestination = Random.insideUnitSphere * patrolRange;
        randomDestination += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDestination, out hit, patrolRange, NavMesh.AllAreas))
        {
            patrolDestination = hit.position;
        }
    }

    void AttackPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (player.position - bulletSpawn.position).normalized * 10f;
    }
}
