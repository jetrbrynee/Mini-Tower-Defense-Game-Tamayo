using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    // Add the onEnemyDestroy event here
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        timeSinceLastSpawn = 0f;

        if (!isSpawning)
            return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
        }
    }

    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, 0.5f));
    }

    private void SpawnEnemy()
    {
        if (enemiesLeftToSpawn > 0)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[randomIndex];
            Instantiate(enemyPrefab, LevelManager.main.startPoint.position, Quaternion.identity);
            enemiesLeftToSpawn--;
            enemiesAlive++;

            // Debug.Log("Spawn Enemy");
        }

        if (enemiesLeftToSpawn == 0 && enemiesAlive == 0)
        {
            currentWave++;
            isSpawning = false;
            Invoke("StartWave", timeBetweenWaves);
        }
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            StartWave();
        }
    }
}
