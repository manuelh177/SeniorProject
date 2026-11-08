
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 0.4f;

    [Header("Visual Feedback")]
    [SerializeField] private GameObject attackIndicator;
    [SerializeField] private float indicatorDuration = 0.15f;

    private float nextAttackTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) &&
            Time.time >= nextAttackTime)
        {
            Attack();
        }
    }

    void Attack()
    {
        nextAttackTime = Time.time + attackCooldown;

        // Flash the red triangle
        if (attackIndicator != null)
        {
            StartCoroutine(FlashIndicator());
        }

        // Find nearby harvestable resources
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange
        );

        foreach (Collider2D hit in hits)
        {
            HarvestableResource resource =
                hit.GetComponentInParent<HarvestableResource>();

            if (resource != null)
            {
                resource.TakeDamage(attackDamage);
            }
        }
    }

    IEnumerator FlashIndicator()
    {
        attackIndicator.SetActive(true);

        yield return new WaitForSeconds(indicatorDuration);

        attackIndicator.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
