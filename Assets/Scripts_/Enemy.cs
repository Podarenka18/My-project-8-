using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private float currentHealth;

    private void Awake()
    {
        if (enemyData != null)
        {
            currentHealth = enemyData.maxHealth;
        }
    }

    public void Die()
    {
        // Отключаем физику и коллайдер
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        Destroy(gameObject, 0.1f);
    }

    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }
}
