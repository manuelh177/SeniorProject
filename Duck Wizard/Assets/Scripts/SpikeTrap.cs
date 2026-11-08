
using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float damageInterval = 1f;

    private Coroutine damageCoroutine;
    private Health playerHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Health health = other.GetComponentInParent<Health>();

        if (health == null)
            return;

        playerHealth = health;

        if (damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(DealDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }

        playerHealth = null;
    }

    private IEnumerator DealDamage()
    {
        while (playerHealth != null && !playerHealth.IsDead)
        {
            playerHealth.TakeDamage(damage);

            yield return new WaitForSeconds(
                Mathf.Max(0.05f, damageInterval)
            );
        }

        damageCoroutine = null;
    }

    private void OnDisable()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }

        playerHealth = null;
    }
}
