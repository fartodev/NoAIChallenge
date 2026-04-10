using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InputAction playerMovement;
    [SerializeField] private InputAction playerAttack;

    private Vector2 moveDirection;
    [Header("Combat Settings")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackDamage = 50f;
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float cappedSpeed = 10f;
    void Update()
    {
        moveDirection = playerMovement.ReadValue<Vector2>();
        if (playerAttack.WasPressedThisFrame())
        {
            PlayerAttack();
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(moveDirection * moveSpeed);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, cappedSpeed);
    }
    private void OnEnable()
    {
        playerMovement.Enable();
        playerAttack.Enable();
    }
    private void OnDisable()
    {
        playerMovement.Disable();
        playerAttack.Disable();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void PlayerAttack()
    {
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in nearbyColliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }
}
