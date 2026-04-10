using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform playerPos;
    [SerializeField] private float spawnRadiusMin = 5f;
    [SerializeField] private float spawnRadiusMax = 10f;
    public float enemyCount = 0f;
    [SerializeField] private float maxEnemyCount = 10f;
    private float spawnTimer;
    [SerializeField] private float spawnInterval = 2f;
    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("No assigned EnemyPrefab");
            return;
        }
        if (playerPos == null)
        {
            Debug.LogError("No assigned PlayerPos");
            return;
        }
    }
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && enemyCount < maxEnemyCount)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }
    private void SpawnEnemy()
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        float spawnDistance = Random.Range(spawnRadiusMin, spawnRadiusMax);

        Vector2 spawnOffset = spawnDirection * spawnDistance;
        Vector2 finalPos = (Vector2)playerPos.position + spawnOffset;

        Instantiate(enemyPrefab, finalPos, UnityEngine.Quaternion.identity);
        enemyCount++;
    }
    private void OnEnable()
    {
        EnemyHealth.onEnemyDeath += HandleEnemyDeath;
    }
    private void OnDisable()
    {
        EnemyHealth.onEnemyDeath -= HandleEnemyDeath;
    }
    private void HandleEnemyDeath()
    {
        enemyCount--;
    }
}
