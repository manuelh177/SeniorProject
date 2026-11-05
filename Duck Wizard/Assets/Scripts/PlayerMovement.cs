
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 dashDirection;

    private bool isSprinting;
    private bool isDashing;

    private float dashTimer;
    private float cooldownTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Read movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontal, vertical).normalized;

        // Sprint input
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Update cooldown timer
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Start dash when Space is pressed
        if (Input.GetKeyDown(KeyCode.Space) &&
            !isDashing &&
            cooldownTimer <= 0f &&
            movement != Vector2.zero)
        {
            StartDash();
        }

        // Update dash duration
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // Dash overrides normal movement
            rb.linearVelocity = dashDirection * dashSpeed;
        }
        else
        {
            // Normal walking or sprinting
            float currentSpeed = isSprinting
                ? sprintSpeed
                : moveSpeed;

            rb.linearVelocity = movement * currentSpeed;
        }
    }

    void StartDash()
    {
        isDashing = true;

        // Save the direction when dash begins
        dashDirection = movement;

        // Reset timers
        dashTimer = dashDuration;
        cooldownTimer = dashCooldown;
    }
}
