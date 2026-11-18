using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;

    private float attackTimer;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        attackTimer += Time.deltaTime;

        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            Debug.Log("Enemy attacks Player!");

            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            attackTimer = 0f;
        }
    }
}