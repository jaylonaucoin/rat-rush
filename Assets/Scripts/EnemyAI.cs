using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    private Transform playerTransform;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator animator;
    private PlayerCharacter playerScript;

    //Patroling
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange = 25;

    //Attacking
    public float timeBetweenAttacks = 3f;
    private bool alreadyAttacked;
    
    //States
    public float sightRange = 15;
    public float attackRange = 1;
    private bool playerInSightRange;
    private bool playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        playerScript = player.GetComponent<PlayerCharacter>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.enabled = true;
        animator.Play("Idle");
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);
        animator.Play("Walk");

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
        animator.Play("Run");
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(playerTransform);

        if (!alreadyAttacked)
        {
            // play the "Stomp" animation for the enemy and make the player take damage
            animator.Play("Stomp");
            playerScript.TakeDamage(1);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
