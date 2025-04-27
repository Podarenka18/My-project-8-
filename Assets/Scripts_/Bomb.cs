using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Настройки взрыва")]
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private float explosionDamage = 50f;
    
    [Header("Спавн нового врага")]
    [SerializeField] private GameObject[] enemyPrefabs; // Массив префабов врагов для спавна
    
    [Header("Layer Settings")]
    [SerializeField] private LayerMask affectedLayers;
    [SerializeField] private LayerMask triggerLayers;

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Проверяем, что столкнулись с нужным слоем
        if (((1 << col.gameObject.layer) & triggerLayers) != 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // 1. Находим всех врагов в радиусе взрыва
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);
        
        foreach (Collider2D hit in colliders)
        {
            // 2. Наносим урон врагам
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GetDamage(explosionDamage);
                
                // 3. Спавним нового врага на месте уничтоженного
                if (enemyPrefabs != null && enemyPrefabs.Length > 0)
                {
                    SpawnNewEnemy(hit.transform.position);
                }
            }
        }
        
        // 4. Уничтожаем бомбу после взрыва
        Destroy(gameObject);
    }

    private void SpawnNewEnemy(Vector2 position)
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[randomIndex], position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}