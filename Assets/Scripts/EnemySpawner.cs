using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab, asteroidPrefab;
    public float spawnInterval = 2f;
    public float spawnRadius = 10f;
    PlayerController player;
    GameManager gameManager;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        gameManager = player.GetComponent<GameManager>();
        // Start spawning enemies at regular intervals
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        
        while(gameManager.gameOver == false)
        {
        yield return new WaitForSeconds(spawnInterval);
        // Generate a random position within the spawn radius
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;

        // Ensure the spawn position is above the ground (adjust as needed)
        spawnPosition.z = 0f;

        // Spawn the enemy at the random position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    
    }
}
