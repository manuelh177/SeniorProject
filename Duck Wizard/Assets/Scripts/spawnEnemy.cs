using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform playerTransform;

    public void SpawnEnemy()
    {
        if (enemyToSpawn == null || playerTransform == null) return;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        float randomDistance = Random.Range(2.5f, 4f);

        Vector3 spawnPosition = playerTransform.position + (Vector3)(randomDirection * randomDistance);

        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        newEnemy.SetActive(true);

        EnemyAI ai = newEnemy.GetComponent<EnemyAI>();
        if (ai != null)
        {
            ai.player = playerTransform;
        }
    }
}