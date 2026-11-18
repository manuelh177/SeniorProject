using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform playerTransform;

    public void SpawnEnemy()
    {
        if (enemyToSpawn == null || playerTransform == null) return;

        Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
        Vector3 spawnPosition = playerTransform.position + randomOffset;

        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

        newEnemy.SetActive(true);

        EnemyAI ai = newEnemy.GetComponent<EnemyAI>();
        if (ai != null)
        {
            ai.player = playerTransform;
        }
    }
}