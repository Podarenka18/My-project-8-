using UnityEngine;

public class PlayerBombSpawner : MonoBehaviour
{
    [Header("Префаб бомбы")]
    [SerializeField] private GameObject bombPrefab; // Перетащи сюда префаб бомбы

    [Header("Настройки спавна")]
    [SerializeField] private Transform spawnPoint; // Точка, откуда вылетает бомба
    [SerializeField] private float throwForce = 5f; // Сила броска
    [SerializeField] private KeyCode spawnKey = KeyCode.Space; // Кнопка для спавна

    private void Update()
    {
        // Если нажата нужная кнопка — спавним бомбу
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnBomb();
        }
    }

    private void SpawnBomb()
    {
        if (bombPrefab == null)
        {
            Debug.LogError("Не назначен префаб бомбы!");
            return;
        }

        // Создаём бомбу
        GameObject bomb = Instantiate(bombPrefab, spawnPoint.position, Quaternion.identity);

        // Добавляем силу броска (если нужно)
        Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
        if (bombRb != null)
        {
            Vector2 throwDirection = Vector2.right * transform.localScale.x; // Бросаем вперёд по направлению игрока
            bombRb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        }
    }
}