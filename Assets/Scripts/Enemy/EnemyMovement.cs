using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform playerPos;
    private Vector2 attackDirection;
    private float attackMoveSpeed = 10f;
    private Vector2 wanderMoveDirection;
    private float wanderMoveSpeed = 5f;
    private float wanderTimer;
    private float wanderInterval = 5f;
    bool isWandering = false;
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerPos == null) Debug.LogError("No Player found");

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos.position);

        if (distanceToPlayer < 5f)
        {
            AttackMove();
            isWandering = false;
        }
        else
        {
            wanderTimer += Time.fixedDeltaTime;
            if (wanderTimer >= wanderInterval || !isWandering)
            {
                wanderMoveDirection = Random.insideUnitCircle.normalized;
                wanderTimer = 0f;
            }

            rb.AddForce(wanderMoveDirection * wanderMoveSpeed);
            isWandering = true;
        }
    }
    private void AttackMove()
    {
        float attackCappedSpeed = 10f;
        attackDirection = playerPos.position - transform.position;

        rb.AddForce(attackDirection.normalized * attackMoveSpeed);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, attackCappedSpeed);
    }
    private void wanderMove()
    {
        float wanderCappedSpeed = 10f;
        wanderMoveDirection = Random.insideUnitCircle.normalized;

        rb.AddForce(wanderMoveDirection * wanderMoveSpeed);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, wanderCappedSpeed);
    }
}
