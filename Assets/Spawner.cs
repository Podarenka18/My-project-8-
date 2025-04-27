using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Header("Префабы для спавна")]
    [SerializeField] private GameObject[] prefabsToSpawn; // Массив префабов

    [Header("Настройки спавна")]
    [SerializeField] private Transform spawnPoint; // Точка спавна (опционально)
    [SerializeField] private Button spawnButton;   // Кнопка для спавна
    [SerializeField] private bool spawnRandomly = true; // Спавнить случайный префаб?

    private int currentPrefabIndex = 0;

    private void Start()
    {
        if (spawnButton != null)
        {
            spawnButton.onClick.AddListener(SpawnPrefab);
        }
        else
        {
            Debug.LogError("Кнопка не назначена в инспекторе!");
        }
    }

    public void SpawnPrefab()
    {
        if (prefabsToSpawn == null || prefabsToSpawn.Length == 0)
        {
            Debug.LogError("Нет префабов для спавна!");
            return;
        }

        // Выбираем префаб (случайный или по порядку)
        GameObject prefabToSpawn;
        if (spawnRandomly)
        {
            prefabToSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        }
        else
        {
            prefabToSpawn = prefabsToSpawn[currentPrefabIndex];
            currentPrefabIndex = (currentPrefabIndex + 1) % prefabsToSpawn.Length;
        }

        // Спавним префаб
        GameObject spawnedObject = Instantiate(prefabToSpawn);

        // Если есть точка спавна, перемещаем объект туда
        if (spawnPoint != null)
        {
            spawnedObject.transform.position = spawnPoint.position;
            spawnedObject.transform.rotation = spawnPoint.rotation;
        }

        Debug.Log($"Создан объект: {prefabToSpawn.name}");
    }
}