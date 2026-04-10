using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public static event System.Action onEnemyDeath;
    [SerializeField] private float maxHealth = 100f;
    private SpriteRenderer spriteRenderer;
    private float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        DamageFlash();
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        onEnemyDeath?.Invoke();
    }
    private void DamageFlash()
    {
        spriteRenderer.color = Color.white;
        Invoke("ResetColor", 0.1f);
    }
    private void ResetColor()
    {
        spriteRenderer.color = Color.red;
    }
}
